using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCv20.Tools.Web {
    /// <summary>
    /// Tipo de mensagem a ser exibida.
    /// </summary>
    public enum MessageType {
        /// <summary>
        /// Mensagem de Erro.
        /// </summary>
        Error,

        /// <summary>
        /// Mensagem de Alera.
        /// </summary>
        Warn,

        /// <summary>
        /// Mensagem de Informação.
        /// </summary>
        Info,
    }
}