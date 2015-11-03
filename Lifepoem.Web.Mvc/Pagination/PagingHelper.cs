using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lifepoem.Web.Mvc.Pagination
{

    public static class PagingHelper
    {
        #region Pagination

        public static MvcHtmlString AjaxPagination(this HtmlHelper html, WebPagingOption option)
        {
            return AjaxPagination(html, option, PagingUIFactory.GetBootstrapPagingUI());
        }

        public static MvcHtmlString AjaxPagination(this HtmlHelper html, WebPagingOption option, PagingUIOption uiOption)
        {
            MvcPaging paging = new MvcPaging()
            {
                PagingOption = option,
                UIOption = uiOption,
                PageUrl = x => string.Format("javascript: Search({0})", x.ToString())
            };
            return paging.PageLinks();
        }

        public static MvcHtmlString HyperlinkPagination(this HtmlHelper html, WebPagingOption option, Func<int, string> pageUrl)
        {
            return HyperlinkPagination(html, option, pageUrl, PagingUIFactory.GetBootstrapPagingUI());
        }

        public static MvcHtmlString HyperlinkPagination(this HtmlHelper html, WebPagingOption option, Func<int, string> pageUrl, PagingUIOption uiOption)
        {
            MvcPaging paging = new MvcPaging()
            {
                PagingOption = option,
                UIOption = uiOption,
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
