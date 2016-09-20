using System;
using System.Text;
using System.Web.Mvc;
using MvcUI.ViewModels;

namespace MvcUI.Helpers
{
	public static class PagingHelper
	{
		public static MvcHtmlString LotsPageLinks(
			this HtmlHelper html,
            LotsViewModel lotsViewModel,
            string updateTagId, 
            Func<int, string> lotsPageUrl)
		{		
			StringBuilder result = new StringBuilder();

			for (int i = 1; i <= lotsViewModel.MaxPageNumber; i++)
			{
				var tag = new TagBuilder("a");
				tag.MergeAttribute("href", lotsPageUrl(i));
				tag.InnerHtml = i.ToString();

                tag.MergeAttribute("data-ajax", "true");
                tag.MergeAttribute("data-ajax-method", "GET");
                tag.MergeAttribute("data-ajax-mode", "replace");
                tag.MergeAttribute("data-ajax-update", "#" + updateTagId);
                //tag.MergeAttribute("onclick", "showAnimate()");

                if (i == lotsViewModel.PageNumber)
				{
					tag.AddCssClass("btn-success");
				}

				result.Append(tag);
			}
			
			return MvcHtmlString.Create(result.ToString());
		}


        //public static MvcHtmlString PageLinks2<T>(
        //    this HtmlHelper html,
        //    LotsViewModel lotsViewModel,
        //    WebViewPage<T> viewModel)
        //{
        //    var str = new StringBuilder();

        //    var lotRequest = new LotsRequestModel
        //    {
        //        PageNumber = 0,
        //        Tab = lotsViewModel.Tab,
        //        ArtworkName = lotsViewModel.ArtworkName,
        //        LotsCountOnPage = lotsViewModel.LotsCountOnPage,
        //        MaxPrice = lotsViewModel.MaxPrice,
        //        MinPrice = lotsViewModel.MinPrice,
        //        OrderByAuctionDate = lotsViewModel.OrderByAuctionDate,
        //        PictureAuthor = lotsViewModel.PictureAuthor
        //    };


        //    for (var i = 1; i <= 3; i++)
        //    {
        //        lotRequest.PageNumber = i;

        //        var link = viewModel.Ajax.ActionLink(i.ToString(),
        //            "Lots", 
        //            "LotManager",
        //            //new { PageNumber = i, LotsCountOnPage = lotsViewModel.CurrentUserId},
        //            lotRequest,
        //            new AjaxOptions
        //            {
        //                UpdateTargetId = "lotList", // <-- DOM element ID to update
        //                InsertionMode = InsertionMode.Replace, // <-- Replace the content of DOM element
        //                HttpMethod = "GET" // <-- HTTP method
        //            });

        //        str.Append(link);
        //    }

        //    return MvcHtmlString.Create(str.ToString());
        //}
    }
}