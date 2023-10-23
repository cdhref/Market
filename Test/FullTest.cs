using Market.Controllers;
using Market.Models;
using Market.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Test
{
    public class FullTest
    {
        private CompanyController _companyController;
        private CategoryController _categoryController;
        private ProductController _productController;
        private CompanyService _companyService;

        // 테스트를 시작하기 전에 호출됩니다.
        public void Setup()
        {
            _companyController = new CompanyController();
            _categoryController = new CategoryController();
            _productController = new ProductController();
            _companyService = new CompanyService();
        }

        // 테스트를 종료하기 전에 호출됩니다.
        public void Teardown()
        {
            _companyController = null;
            _categoryController = null;
            _productController = null;
            _companyService = null;
        }

        [TestMethod]
        public void GetProducts_Success()
        {
            // 1.企業登録
            string testCorpName = "test企業";
            string testCorpComment = "testコメント";
            string testCorpIncorporationAt = "2023-10-21";
            CompanyModel company = new CompanyModel();
            company.Name = testCorpName;
            company.Comment = testCorpComment;
            company.IncorporationAt = testCorpIncorporationAt;
            _companyController.Add(company);

            // 2.企業情報照会
            List<CompanyModel> companyList = _companyService.GetCompanyList();
            _companyService.GetCompany(companyList[0].Id);
            companyList[0].Name = "修正した企業名";

            // 3.企業情報修正
            _companyService.ModifyCompany(companyList[0]);
            
            // 3.企業情報削除
            _companyService.DeleteCompany(companyList[0]);

            
            
            // カテゴリ、商品関連のテストはSampleのため省略
        }
    }
}
