using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Lifepoem.Web.Mvc.Pagination
{
    /// <summary>
    /// Create Pagination using:
    /// In order to support displaying custom information in left or right, we need to add table layout
    /// Because the pagination has a "display:inline-block" attribute, make it impossible to display info in the same line.
    /// 
    ///     <span class="custominfo">custom information</span>
    ///     <ul>
    ///         <li><a href="#">First</a></li>
    ///         <li><a href="#">Previous</a></li>
    ///         <li><a href="#">...</a></li>
    ///         <li><a href="#">1</a></li>
    ///         <li class="active"><a disabled="disabled" href="#">2</a></li>
    ///         <li><a href="#">3</a></li>
    ///         <li><a href="#">4</a></li>
    ///         <li><a href="#">5</a></li>
    ///         <li><a href="#">...</a></li>
    ///         <li><a href="#">Next</a></li>
    ///         <li><a href="#">Last</a></li>
    ///     </ul>
    ///     
    /// </summary>
    public class MvcPaging
    {
        public WebPagingOption PagingOption { get; set; }
        public PagingUIOption UIOption { get; set; }
        public Func<int, string> PageUrl { get; set; }

        public MvcPaging() { }

        public MvcHtmlString PageLinks()
        {
            if (PagingOption == null || PagingOption.TotalPages <= 1)
            {
                return new MvcHtmlString("");
            }

            TagBuilder ulTag = new TagBuilder("ul");
            AddCssClass(ulTag, UIOption.CssClass);
            ulTag.InnerHtml = GetPageLinks();

            string customInfo = GetCustomInfoHTML();

            string result = UIOption.CustomInfoPosition == CustomInfoPosition.Left ? "{0}{1}" : "{1}{0}";
            result = string.Format(result, customInfo, ulTag.ToString());
            return new MvcHtmlString(result.ToString());
        }

        private string GetPageLinks()
        {
            StringBuilder links = new StringBuilder();

            links.Append(GetPageURL(1, UIOption.FirstPageText));
            links.Append(GetPageURL(PagingOption.CurrentPage - 1, UIOption.PrevPageText));

            int start = PagingOption.CurrentPage / UIOption.TotalPageLink;
            if (PagingOption.CurrentPage % UIOption.TotalPageLink == 0) start -= 1;
            start = start * UIOption.TotalPageLink + 1;
            int end = start + UIOption.TotalPageLink - 1;
            if (end > PagingOption.TotalPages) end = PagingOption.TotalPages;

            if (start > UIOption.TotalPageLink)
            {
                links.Append(GetPageURL(start - 1, "..."));
            }

            for (int i = start; i <= end; i++)
            {
                links.Append(GetPageURL(i, i.ToString()));
            }

            if (end < PagingOption.TotalPages)
            {
                links.Append(GetPageURL(end + 1, "..."));
            }
            links.Append(GetPageURL(PagingOption.CurrentPage + 1, UIOption.NextPageText));
            links.Append(GetPageURL(PagingOption.TotalPages, UIOption.LastPageText));

            return links.ToString();
        }

        private string GetCustomInfoHTML()
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(UIOption.CustomInfoHTML))
            {
                TagBuilder tag = new TagBuilder("span");

                string customInfo = UIOption.CustomInfoHTML;
                customInfo = customInfo.Replace("{TotalItems}", PagingOption.TotalItems.ToString());
                customInfo = customInfo.Replace("{TotalPages}", PagingOption.TotalPages.ToString());
                customInfo = customInfo.Replace("{CurrentPage}", PagingOption.CurrentPage.ToString());
                customInfo = customInfo.Replace("{ItemsPerPage}", PagingOption.ItemsPerPage.ToString());
                AddCssClass(tag, UIOption.CustomInfoCssClass);
                tag.InnerHtml = customInfo;

                result = tag.ToString();
            }

            return result;
        }

        public string GetPageURL(int page, string pageText)
        {
            if (string.IsNullOrEmpty(pageText))
            {
                return string.Empty;
            }

            if (page < 1) page = 1;
            if (page > PagingOption.TotalPages) page = PagingOption.TotalPages;

            TagBuilder liTag = new TagBuilder("li");
            AddCssClass(liTag, UIOption.PageCssClass);
            if (PagingOption.CurrentPage == page)
            {
                if (page.ToString() == pageText)
                {
                    AddCssClass(liTag, UIOption.CurrentPageCssClass);
                }
                else
                {
                    AddCssClass(liTag, UIOption.DisablePageCssClass);
                }
            }

            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", PageUrl(page));
            tag.InnerHtml = string.IsNullOrEmpty(pageText) ? page.ToString() : pageText;
            if (PagingOption.CurrentPage == page)
            {
                tag.MergeAttribute("disabled", "disabled");
            }

            liTag.InnerHtml = tag.ToString();
            return liTag.ToString();
        }

        private void AddCssClass(TagBuilder tag, string cssClass)
        {
            if (!string.IsNullOrEmpty(cssClass))
            {
                tag.AddCssClass(cssClass);
            }
        }
    }
}
