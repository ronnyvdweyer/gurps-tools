using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCv20.Tools.Core;
using System.Collections;

namespace SCv20.Tools.Web.Views.Shared {
    public partial class Pager : UserControlBase {
        public delegate void PageIndexChangedHandler(object sender, PageIndexChangedEventArgs e);
        public event PageIndexChangedHandler PageIndexChanged;



        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack)
                currentPage.Text = "1";
        }



        protected void Next_Click(object sender, EventArgs e) {
            CurrentPageIndex++;

            if (CurrentPageIndex > TotalPages)
                CurrentPageIndex = TotalPages;

            var args = new PageIndexChangedEventArgs(CurrentPageIndex, TotalPages, TotalRecords);
            
            OnPageIndexChanged(args);
        }



        protected void Prev_Click(object sender, EventArgs e) {
            CurrentPageIndex--;

            if (CurrentPageIndex < 0)
                CurrentPageIndex = 0;

            var args = new PageIndexChangedEventArgs(CurrentPageIndex, TotalPages, TotalRecords);
            
            OnPageIndexChanged(args);
        }



        protected void CurrentPage_Change(object sender, EventArgs e) {
            CurrentPageIndex = currentPage.Text.SafeInt32() - 1;

            if (CurrentPageIndex < 0) {
                CurrentPageIndex = 0;
            }

            if (CurrentPageIndex > TotalPages-1) {
                CurrentPageIndex = TotalPages-1;
            }

            //currentPage.Text = (CurrentPageIndex + 1).ToString();

            var args = new PageIndexChangedEventArgs(CurrentPageIndex, TotalPages, TotalRecords);
            OnPageIndexChanged(args);
        }
        
        
        
        
        
        
        public PagedList<T> PaginateDataSource<T>(IQueryable<T> source) {
            var paged = new PagedList<T>(source, CurrentPageIndex, PageSize);

            TotalPages = paged.TotalPages;
            TotalRecords = paged.TotalRecords;

            DisableNavigation(paged.HasPreviousPage, paged.HasNextPage);

            return paged;
        }



        public PagedList<T> PaginateDataSource<T>(IList<T> source) {
            var paged = new PagedList<T>(source, CurrentPageIndex, PageSize);

            TotalPages = paged.TotalPages;
            TotalRecords = paged.TotalRecords;
            
            DisableNavigation(paged.HasPreviousPage, paged.HasNextPage);

            return paged;
        }



        public PagedList<T> PaginateDataSource<T>(IEnumerable<T> source) {
            var paged = new PagedList<T>(source, CurrentPageIndex, PageSize, source.ToList().Count());

            TotalPages = paged.TotalPages;
            TotalRecords = paged.TotalRecords;

            DisableNavigation(paged.HasPreviousPage, paged.HasNextPage);
            
            return paged;
        }
        
        
        
        
        
        
        private void DisableNavigation(bool hasPreviousPage, bool hasNextPage) {
            prevPage.Enabled = hasPreviousPage;
            nextPage.Enabled = hasNextPage;
        }



        private void OnPageIndexChanged(PageIndexChangedEventArgs args) {
            if (PageIndexChanged != null)
                PageIndexChanged(this, args);
            else
                throw new NullReferenceException("Evento {0}.OnPageIndexChanged() não registrado.".FormatWith(this.ID));
        }
        
        
        
        
        
        
        public int PageSize {
            get;
            set;
        }
        


        public int CurrentPageIndex {
            get {
                return currentPageIndex.Value.SafeInt32();
            }
            set {
                currentPage.Text = (value + 1).ToString();
                currentPageIndex.Value = value.ToString();
            }
        }



        public int TotalPages {
            get {
                return totalPages.Value.SafeInt32();
            }
            private set {
                totalPages.Value = value.ToString();
            }
        }



        public int TotalRecords {
            get {
                return totalRecords.Value.SafeInt32();
            }
            set {
                totalRecords.Value = value.ToString();
            }
        }


        public string PagerTitle {
            set;
            get;
        }


        public string PagerToken {
            set;
            get;
        }


        /// <summary>
        /// Nested Class
        /// </summary>
        [Serializable]
        public class PageIndexChangedEventArgs : EventArgs {
            public PageIndexChangedEventArgs(int pageIndex, int totalPages, int totalRecords) {
                PageIndex = pageIndex+1;
                TotalPages = totalPages;
                TotalRecords = totalRecords;
            }


            /// <summary>
            /// Número da página atual.
            /// </summary>
            public int PageIndex {
                get;
                private set;
            }


            /// <summary>
            /// Quatidade total de páginas.
            /// </summary>
            public int TotalPages {
                get;
                private set;
            }


            /// <summary>
            /// Quantidade total de registros.
            /// </summary>
            public int TotalRecords {
                get;
                private set;
            }
        }
    }
}