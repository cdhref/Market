using Market.Service;
using System.Web.Mvc;
using System;
using Market.Models;
using Market.Models.Common;
using Market.Object.Constant;
using Market.Common.Constant;
using System.Diagnostics;
using Market.Common.Util;

namespace Market.Controllers
{
    public class CompanyController : Controller
    {
        private CompanyService companyService;

        /// <summary>
        /// 企業データ関連Controller
        /// </summary>
        public CompanyController()
        {
            companyService = new CompanyService();
        }

        /// <summary>
        /// 企業情報リスト閲覧画面
        /// </summary>
        /// <returns>企業情報閲覧ページ</returns>
        [HttpGet]
        public ActionResult List()
        {
            ViewBag.Title = ViewPageTitle.companyList;
            // sampleコードのためPaging処理は除外。今後入れたくなるかも知れないのでParameterはもらって置く。
            string key = Message.pageKey;
            string strPage = Request.Form[Message.pageKey];
            ViewBag.companyList = companyService.GetCompanyList(strPage);

            return View();
        }

        /// <summary>
        /// 企業情報登録ページに移動するための
        /// </summary>
        /// <returns>企業情報入力ページ</returns>
        [HttpGet]
        public ActionResult AddPage()
        {
            ViewBag.Title = ViewPageTitle.companyAdd;
            return View("Add");
        }

        /// <summary>
        /// 企業情報を登録する
        /// </summary>
        /// <param name="company">Model</param>
        /// <returns>json reselt</returns>
        [HttpPost]
        public ActionResult Add(CompanyModel company)
        {
            if (!company.CheckNotNullDataAndSetDefault())
            {

                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.errNotAvailableParameter));
            }
            try
            {
                companyService.AddCompany(company);
            }
            catch (Exception e)
            {
                Trace.WriteLine(ErrorMessageUtil.GetServerErrorMessage(e));
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.failedUpsert));
            }

            return Json(new JsonResponseWrapper(ErrorCode.OK, Message.addSuccessMessage));
        }

        /// <summary>
        /// 企業情報を修正する
        /// </summary>
        /// <param name="company">Model</param>
        /// <returns>登録結果</returns>
        [HttpPost]
        public ActionResult Modify(CompanyModel company)
        {
            if (!company.CheckNotNullDataAndSetDefault())
            {
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.errNotAvailableParameter));
            }
            try
            {
                companyService.ModifyCompany(company);
            }
            catch (Exception e)
            {
                Trace.WriteLine(ErrorMessageUtil.GetServerErrorMessage(e));
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
            ViewBag.Title = ViewPageTitle.companyAdd;
            ViewBag.companyData = companyService.GetCompany(id);
            return View("Add");
        }

        /// <summary>
        /// 企業情報を削除する
        /// </summary>
        /// <param name="company">企業情報</param>
        /// <returns>削除結果</returns>
        [HttpPost]
        public ActionResult Delete(CompanyModel company)
        {
            try
            {
                companyService.DeleteCompany(company);
            }
            catch (Exception e)
            {
                Trace.WriteLine(ErrorMessageUtil.GetServerErrorMessage(e));
                return Json(new JsonResponseWrapper(ErrorCode.BAD_REQUEST, Message.failedUpsert));
            }

            return Json(new JsonResponseWrapper(ErrorCode.OK, Message.deleteSuccessMessage));
        }
    }
}
