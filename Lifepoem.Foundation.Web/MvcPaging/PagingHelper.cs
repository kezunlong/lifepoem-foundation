using Lifepoem.Foundation.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lifepoem.Foundation.Web.MVC
{

    public static class PagingHelper
    {

        #region Pagination

        /// <summary>
        /// Pagination Layout in CDC web page:
        /// <div>
        ///     <div class="pull-right">
        ///         @Html.AjaxPagination(Model.PagingOption)
        ///     </div>
        ///     <div class="clearfix"></div>
        /// </div>
        /// 
        /// Pagination will be composed by a two columns table,
        /// one cell is for custom info, and the other is for ul list of hyperlinks.
        /// 
        /// If we need to add other information after the page links, we should use this method to do clearfix.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static MvcHtmlString CdcAjaxPagination(this HtmlHelper html, WebPagingOption option)
        {
            var divContainer = new TagBuilder("div");
            var divRight = new TagBuilder("div");
            divRight.AddCssClass("pull-right");
            divRight.InnerHtml = AjaxPagination(html, option).ToHtmlString();
            var divClearFix = new TagBuilder("div");
            divClearFix.AddCssClass("clearfix");
            divContainer.InnerHtml = divRight.ToString() + divClearFix.ToString();
            return new MvcHtmlString(divContainer.ToString());
        }

        public static MvcHtmlString AjaxPagination(this HtmlHelper html, WebPagingOption option)
        {
            MvcPaging paging = new MvcPaging()
            {
                PagingOption = option,
                UIOption = PagingUIFactory.GetBootstrapPagingUI(),
                PageUrl = x => string.Format("javascript: Search({0})", x.ToString())
            };
            return paging.PageLinks();
        }

        public static MvcHtmlString CdcHyperlinkPagination(this HtmlHelper html, WebPagingOption option, Func<int, string> pageUrl)
        {
            var divContainer = new TagBuilder("div");
            var divRight = new TagBuilder("div");
            divRight.AddCssClass("pull-right");
            divRight.InnerHtml = HyperlinkPagination(html, option, pageUrl).ToHtmlString();
            var divClearFix = new TagBuilder("div");
            divClearFix.AddCssClass("clearfix");
            divContainer.InnerHtml = divRight.ToString() + divClearFix.ToString();
            return new MvcHtmlString(divContainer.ToString());
        }

        public static MvcHtmlString HyperlinkPagination(this HtmlHelper html, WebPagingOption option, Func<int, string> pageUrl)
        {
            MvcPaging paging = new MvcPaging()
            {
                PagingOption = option,
                UIOption = PagingUIFactory.GetBootstrapPagingUI(),
                PageUrl = pageUrl
            };
            return paging.PageLinks();
        }

        public static string GetUrl(this HtmlHelper html, int index)
        {
            System.Collections.Specialized.NameValueCollection querystring = HttpContext.Current.Request.QueryString;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("?").AppendFormat("page=" + index);
            foreach (string key in querystring.Keys)
            {
                if (key != "page")
                    sb.AppendFormat("&{0}={1}", key, HttpContext.Current.Server.UrlEncode(querystring[key]));
            }
            return sb.ToString();
        }

        #endregion
    }
}
