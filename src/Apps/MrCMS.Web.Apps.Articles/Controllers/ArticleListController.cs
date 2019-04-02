﻿using Microsoft.AspNetCore.Mvc;
using MrCMS.Attributes;
using MrCMS.Web.Apps.Articles.ModelBinders;
using MrCMS.Web.Apps.Articles.Models;
using MrCMS.Web.Apps.Articles.Pages;
using MrCMS.Web.Apps.Articles.Services;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Apps.Articles.Controllers
{
    public class ArticleListController : MrCMSAppUIController<MrCMSArticlesApp>
    {
        private readonly IArticleListService _articleListService;

        public ArticleListController(IArticleListService articleListService)
        {
            _articleListService = articleListService;
        }

        [CanonicalLinks("paged-articles")]
        public ActionResult Show(ArticleList page, 
            [ModelBinder(typeof(ArticleListModelBinder))] ArticleSearchModel model)
        {
            ViewData["paged-articles"] = _articleListService.GetArticles(page, model);
            ViewData["article-search-model"] = model;

            return View(page);
        }
    }
}
