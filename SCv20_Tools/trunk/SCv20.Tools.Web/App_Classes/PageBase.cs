using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.IO;
using System.Reflection;
using SCv20.Tools.Core.Services;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using SCv20.Tools.Web.App_Classes;


namespace SCv20.Tools.Web {
    public abstract class PageBase : System.Web.UI.Page {

        /// <summary>
        /// Recupera um Script (texto) definido como EmbedResource no assembly atual.
        /// </summary>
        /// <param name="resourceID">Nome completamente qualificado do EmbededResource.</param>
        /// <returns></returns>
        protected string GetEmbededScript(string resourceID) {
            var assembly = Assembly.GetExecutingAssembly();

            try {
                var stream = new StreamReader(assembly.GetManifestResourceStream(resourceID));
                var contents = stream.ReadToEnd();
                stream.Close();
                stream.Dispose();
                return contents;
            }
            catch (ArgumentNullException ex) {
                throw new InvalidOperationException("Requested Resource [{0}] not found in [{1}].".FormatWith(resourceID, assembly.FullName), ex);
            }
        }


        /// <summary>
        /// Adiciona uma mensagem do lado client da requisição.
        /// </summary>
        /// <param name="message">Mensagem a ser exibida.</param>
        /// <param name="messageType">Tipo da Mensagem a ser exibida.</param>
        protected void AddClientMessage(string message, MessageType messageType) {
            var scriptId = this.ClientID + "_Script";
            var script = GetEmbededScript("SCv20.Tools.Web.App_Classes.Resources.AddClientMessage.js");

            script = script.Replace("{#message}", message)
                           .Replace("{#messageType}", messageType.ToString().ToLower());

            ScriptManager.RegisterStartupScript(this.Page, typeof(UserControlBase),
                scriptId, script,
                true);
        }


        /// <summary>
        /// Adiciona uma mensagem do lado client da requisição.
        /// </summary>
        /// <param name="message">Mensagem a ser exibida.</param>
        protected void AddClientMessage(string message) {
            AddClientMessage(message, MessageType.Info);
        }


        /// <summary>
        /// Valida um Objeto contedo data anotations e adiciona o resultado à coleção de validadores da página.
        /// </summary>
        /// <param name="dataAnotatedObject">Objeto contentod DataAnotations.</param>
        public void ValidateAnnotations(object dataAnotatedObject) {
            //  http://stackoverflow.com/questions/3089760/using-asp-net-mvc-data-annotation-outside-of-mvc
            //  http://stackoverflow.com/questions/777889/on-postback-how-can-i-add-a-error-message-to-validation-summary
            //  http://blog.webmastersam.net/post/Adding-custom-error-message-to-ValidationSummary-without-validators.aspx
            //  http://stackoverflow.com/questions/7149899/custom-validation-not-executing
            
            var results = new List<ValidationResult>();
            var context = new ValidationContext(dataAnotatedObject, null, null);
            var isValid = Validator.TryValidateObject(dataAnotatedObject, context, results, true);

            var panel = Page.Master.FindControl("errorPanel");
            var error = Page.Master.FindControl("errorSummary") as ValidationSummary;
            error.ValidationGroup = "DataAnotationsValidator";

            foreach (var v in results) {
                var validator = new CustomValidator();
                validator.ValidationGroup = "DataAnotationsValidator";
                validator.IsValid = false;
                validator.ErrorMessage = v.ErrorMessage;
                
                Page.Validators.Add(validator);
            }
            if (results.Count > 0)
                panel.Visible = true;
            else
                panel.Visible = false;
        }


        /// <summary>
        /// Obtém uma referência ao serviço global de dados.
        /// </summary>
        protected virtual DataService DataService {
            get {
                return SCv20.Tools.Core.Services.DataService.GetInstance();
            }
        }


        /// <summary>
        /// Obtem uma referência ao valores armazenados na sessão.
        /// </summary>
        protected virtual SessionVariables SessionVariables {
            get {
                return new SessionVariables();
            }
        }


        /// <summary>
        /// Método padrão reponsável por carregar os dados em uma página.
        /// </summary>
        protected abstract void LoadPageData();
    }
}