using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCv20.Tools.Core {
    /// <summary>
    /// Paged list interface
    /// </summary>
    public interface IPagedList<T> : IList<T> {
        int PageIndex {
            get;
        }

        int PageSize {
            get;
        }

        int TotalRecords {
            get;
        }

        int TotalPages {
            get;
        }

        bool HasPreviousPage {
            get;
        }

        bool HasNextPage {
            get;
        }
    } 
}
