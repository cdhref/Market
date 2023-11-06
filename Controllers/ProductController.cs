using System.Web.Mvc;
using System;
using Market.Service;
using Market.Common.Constant;
using Market.Object.Constant;
using Market.Models;
using Market.Models.Common;

namespace Market.Controllers
{
    public class ProductController : Controller
    {
        private readonly CompanyService companyService;
        private readonly CategoryService categoryService;
        private readonly ProductService productService;

        /// <summary>
        /// 企業データ関連Controller
        /// </summary>
        public ProductController()
        {
            companyService = new CompanyService();
            categoryService = new CategoryService();
            productService = new ProductService();
        }

        /// <summary>
        /// 企業情報リスト閲覧画面
        /// </summary>
        /// <returns>企業情報閲覧ページ</returns>
        [HttpGet]
        public ActionResult List()
        {
            ViewBag.Title = ViewPageTitle.productList;
            // sampleコードのためPaging処理は除外。今後入れたくなるかも知れないのでParameterはもらって置く。
            string key = Message.pageKey;
            string strPage = Request.Form[Message.pageKey];
            ViewBag.productList = productService.GetProductList(strPage);

            return View();
        }

        /// <summary>
        /// 企業情報登録ページに移動するための
        /// </summary>
        /// <returns>企業情報入力ページ</returns>
        [HttpGet]
        public ActionResult AddPage()
        {
            ViewBag.Title = ViewPageTitle.productAdd;
            ViewBag.categoryList = categoryService.GetCategoryList();
            ViewBag.companyList = companyService.GetCompanyList();
            return View("Add");
        }

        /// <summary>
        /// 企業情報を登録する
        /// </summary>
        /// <param name="product">Model</param>
        /// <returns>json reselt</returns>
        [HttpPost]
        public ActionResult Add(ProductModel product)
        {
            if (!product.CheckNotNullDataAndSetDefault())
            {

                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.errNotAvailableParameter));
            }
            try
            {
                productService.AddProduct(product);
            }
            catch (Exception)
            {
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.failedUpsert));
            }

            return Json(new JsonResponseWrapper(ErrorCode.OK, Message.addSuccessMessage));
        }

        /// <summary>
        /// 企業情報を修正する
        /// </summary>
        /// <param name="product">Model</param>
        /// <returns>登録結果</returns>
        [HttpPost]
        public ActionResult Modify(ProductModel product)
        {
            if (!product.CheckNotNullDataAndSetDefault())
            {
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.errNotAvailableParameter));
            }
            try
            {
                productService.ModifyProduct(product);
            }
            catch (Exception)
            {
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.failedUpsert));
            }

            return Json(new JsonResponseWrapper(ErrorCode.OK, Message.modifySuccessMessage));
        }

        /// <summary>
        /// 企業の詳細情報閲覧画面
        /// </summary>
        /// <param name="id">db pk</param>
        /// <returns>企業の詳細情報閲覧ページ</returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            ViewBag.Title = ViewPageTitle.categoryAdd;
            ViewBag.productData = productService.GetProduct(id);
            return View("Add");
        }

        /// <summary>
        /// 企業情報を削除する
        /// </summary>
        /// <param name="product">企業情報</param>
        /// <returns>削除結果</returns>
        [HttpPost]
        public ActionResult Delete(ProductModel product)
        {
            try
            {
                productService.DeleteProduct(product);
            }
            catch (Exception)
            {
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.failedUpsert));
            }

            return Json(new JsonResponseWrapper(ErrorCode.OK, Message.deleteSuccessMessage));
        }
    }
}
