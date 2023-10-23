using Market.Common.Constant;
using Market.Models;
using Market.Models.Common;
using Market.Object.Constant;
using Market.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Market.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;

        /// <summary>
        /// カテゴリデータ関連Controller
        /// </summary>
        public CategoryController()
        {
            categoryService = new CategoryService();
        }

        /// <summary>
        /// カテゴリ情報リスト閲覧画面
        /// </summary>
        /// <returns>カテゴリ情報閲覧ページ</returns>
        [HttpGet]
        public IActionResult List()
        {
            ViewBag.Title = ViewPageTitle.categoryList;
            // sampleコードのためPaging処理は除外。今後入れたくなるかも知れないのでParameterはもらって置く。
            string key = Message.pageKey;
            string strPage = Request.Query[Message.pageKey];
            ViewBag.categoryList = categoryService.GetCategoryList(strPage);

            return View();
        }

        /// <summary>
        /// カテゴリ情報登録ページに移動するための
        /// </summary>
        /// <returns>カテゴリ情報入力ページ</returns>
        [HttpGet]
        public IActionResult AddPage()
        {
            ViewBag.Title = ViewPageTitle.categoryAdd;
            return View("Add");
        }

        /// <summary>
        /// カテゴリ情報を登録する
        /// </summary>
        /// <param name="category">Model</param>
        /// <returns>json reselt</returns>
        [HttpPost]
        public IActionResult Add([FromBody] CategoryModel category)
        {
            if (!category.CheckNotNullDataAndSetDefault())
            {

                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.errNotAvailableParameter));
            }
            try
            {
                categoryService.AddCategory(category);
            }
            catch (Exception)
            {
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.failedUpsert));
            }

            return Json(new JsonResponseWrapper(ErrorCode.OK, Message.addSuccessMessage));
        }

        /// <summary>
        /// カテゴリ情報を修正する
        /// </summary>
        /// <param name="category">Model</param>
        /// <returns>登録結果</returns>
        [HttpPost]
        public IActionResult Modify([FromBody] CategoryModel category)
        {
            if (!category.CheckNotNullDataAndSetDefault())
            {
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.errNotAvailableParameter));
            }
            try
            {
                categoryService.ModifyCategory(category);
            }
            catch (Exception)
            {
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.failedUpsert));
            }

            return Json(new JsonResponseWrapper(ErrorCode.OK, Message.modifySuccessMessage));
        }

        /// <summary>
        /// カテゴリの詳細情報閲覧画面
        /// </summary>
        /// <param name="id">db pk</param>
        /// <returns>カテゴリの詳細情報閲覧ページ</returns>
        [HttpGet]
        public IActionResult Detail(int id)
        {
            ViewBag.Title = ViewPageTitle.categoryAdd;
            ViewBag.categoryData = categoryService.GetCategory(id);
            return View("Add");
        }

        /// <summary>
        /// カテゴリ情報を削除する
        /// </summary>
        /// <param name="category">カテゴリ情報</param>
        /// <returns>削除結果</returns>
        [HttpPost]
        public IActionResult Delete([FromBody] CategoryModel category)
        {
            try
            {
                categoryService.DeleteCategory(category);
            }
            catch (Exception)
            {
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.failedUpsert));
            }

            return Json(new JsonResponseWrapper(ErrorCode.OK, Message.deleteSuccessMessage));
        }
    }
}
