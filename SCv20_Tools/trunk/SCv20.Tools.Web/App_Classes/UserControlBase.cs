using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.IO;
using System.Reflection;
using SCv20.Tools.Core.Services;
using SCv20.Tools.Web.App_Classes;


namespace SCv20.Tools.Web {
    public class UserControlBase : System.Web.UI.UserControl {

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
            var script   = GetEmbededScript("SCv20.Tools.Web.App_Classes.Resources.AddClientMessage.js");

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
                return new SessionVariables() ;
            }
        }
    }
}