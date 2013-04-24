using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCv20.Tools.Core {
    /// <summary>
    /// Classe Adapter para transformar diversos tipos de coleções em coleções paginadas.
    /// </summary>
    /// <typeparam name="T">Tipo de coleção a ser paginada.</typeparam>
    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T> {


        /// <summary>
        /// Inicializa uma nova instância da classe PagedList.
        /// </summary>
        /// <param name="source">Fonte de dados implementada pela a interface IQueryable.</param>
        /// <param name="pageIndex">Posição atual da página. Esse valor é baseado em zero.</param>
        /// <param name="pageSize">Quantidade de registros por página.</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize) {
            int total = source.Count();
            this.TotalRecords = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }


        /// <summary>
        /// Inicializa uma nova instância da classe PagedList.
        /// </summary>
        /// <param name="source">Fonte de dados implementada pela a interface IList.</param>
        /// <param name="pageIndex">Posição atual da página. Esse valor é baseado em zero.</param>
        /// <param name="pageSize">Quantidade de registros por página.</param>
        public PagedList(IList<T> source, int pageIndex, int pageSize) {
            TotalRecords = source.Count();
            TotalPages = TotalRecords / pageSize;

            if (TotalRecords % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }


        /// <summary>
        /// Inicializa uma nova instância da classe PagedList. Esse método é adequado ao uso por arrays e coleções de objetos anônimos.
        /// </summary>
        /// <param name="source">Fonte de dados implementada pela a interface IEnumerable.</param>
        /// <param name="pageIndex">Posição atual da página. Esse valor é baseado em zero.</param>
        /// <param name="pageSize">Quantidade de registros por página.</param>
        /// <param name="totalCount">Quantidade total de registros da fonte dados.</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount) {
            TotalRecords = totalCount;
            TotalPages = TotalRecords / pageSize;

            if (TotalRecords % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize));
        }


        /// <summary>
        /// Recupera ó número da página corrente. Esse valor é baseado em zero.
        /// </summary>
        public int PageIndex {
            get;
            private set;
        }
        

        /// <summary>
        /// Recupera a quantidade de registros configurada por página.
        /// </summary>
        public int PageSize {
            get;
            private set;
        }
        

        /// <summary>
        /// Recupera a quantidade total de registros da fonte de dados especificada.
        /// </summary>
        public int TotalRecords {
            get;
            private set;
        }
        

        /// <summary>
        /// Recupera o número total de páginas existentes na fonte de dados calculado de acordo com o PageSize.
        /// </summary>
        public int TotalPages {
            get;
            private set;
        }


        /// <summary>
        /// Indica se existe ou não páginas anteriores à pagina atual.
        /// </summary>
        public bool HasPreviousPage {
            get {
                return (PageIndex > 0);
            }
        }


        /// <summary>
        /// Indica se existe ou não páginas posteriores à página atual.
        /// </summary>
        public bool HasNextPage {
            get {
                return (PageIndex + 1 < TotalPages);
            }
        }
    }
}
