using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifepoem.Web.Mvc.Pagination
{
    public class PagingUIOption
    {
        public string FirstPageText { get; set; }
        public string PrevPageText { get; set; }
        public string NextPageText { get; set; }
        public string LastPageText { get; set; }
        
        public int TotalPageLink { get; set; }
        
        public string CssClass { get; set; }
        public string PageCssClass { get; set; }
        public string CurrentPageCssClass { get; set; }
        public string DisablePageCssClass { get; set; }

        /// <summary>
        /// In addition to static text, variables like {TotalItems}, {TotalPages}, {CurrentPage}, {ItemsPerPage} can be included
        /// </summary>
        public string CustomInfoHTML { get; set; }
        public CustomInfoPosition CustomInfoPosition { get; set; }
        public string CustomInfoCssClass { get; set; }
    }

    public enum CustomInfoPosition
    {
        Left,
        Right
    }

    public class PagingUIFactory
    {
        public static PagingUIOption GetDefaultPagingUI()
        {
            return new PagingUIOption
            {
                FirstPageText = "First",
                PrevPageText = "Previous",
                NextPageText = "Next",
                LastPageText = "Last",
                CssClass = "pagination pagination-sm",
                PageCssClass = "",
                CurrentPageCssClass = "active",
                DisablePageCssClass = "disabled",
                TotalPageLink = 10,
                CustomInfoHTML = string.Empty,
                CustomInfoCssClass = "custominfo"
            };
        }

        public static PagingUIOption GetBootstrapPagingUI()
        {
            return new PagingUIOption
            {
                FirstPageText = "<span class='glyphicon glyphicon-step-backward'></span>",
                PrevPageText = "<span class='glyphicon glyphicon-chevron-left'></span>",
                NextPageText = "<span class='glyphicon glyphicon-chevron-right'></span>",
                LastPageText = "<span class='glyphicon glyphicon-step-forward'></span>",
                CssClass = "pagination pagination-sm",
                PageCssClass = "",
                CurrentPageCssClass = "active",
                DisablePageCssClass = "disabled",
                TotalPageLink = 10,
                CustomInfoHTML = "Records: {TotalItems}, Pages: {TotalPages}",
                CustomInfoCssClass = "custominfo"
            };
        }

        public static PagingUIOption GetFontAwesomeUI()
        {
            return new PagingUIOption
            {
                FirstPageText = "<span class='icon fa fa-step-backward'></span>",
                PrevPageText = "<span class='icon fa fa-chevron-left'></span>",
                NextPageText = "<span class='icon fa fa-chevron-right'></span>",
                LastPageText = "<span class='icon fa fa-step-forward'></span>",
                CssClass = "pagination pagination-sm",
                PageCssClass = "",
                CurrentPageCssClass = "active",
                DisablePageCssClass = "disabled",
                TotalPageLink = 10,
                CustomInfoHTML = "Records: {TotalItems}, Pages: {TotalPages}",
                CustomInfoCssClass = "custominfo"
            };
        }
    }
}
