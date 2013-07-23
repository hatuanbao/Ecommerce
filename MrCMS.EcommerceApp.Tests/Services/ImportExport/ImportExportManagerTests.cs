﻿using System.IO;
using FakeItEasy;
using FluentAssertions;
using MrCMS.Web.Apps.Ecommerce.Services.ImportExport;
using MrCMS.Web.Apps.Ecommerce.Services.Products;
using Xunit;

namespace MrCMS.EcommerceApp.Tests.Services.ImportExport
{
    public class ImportExportManagerTests : InMemoryDatabaseTest
    {
        private IImportProductsValidationService _importProductsValidationService;
        private IImportProductsService _importProductsService;
        private IProductVariantService _productVariantService;
        private ImportExportManager _importExportManager;

        public ImportExportManagerTests()
        {
            _importProductsValidationService = A.Fake<IImportProductsValidationService>();
            _importProductsService = A.Fake<IImportProductsService>();
            _productVariantService = A.Fake<IProductVariantService>();

            _importExportManager = new ImportExportManager(_importProductsValidationService, _importProductsService, _productVariantService);
        }

        [Fact]
        public void ImportExportManager_ImportProductsFromExcel_ShouldNotBeNull()
        {
            var result = _importExportManager.ImportProductsFromExcel(GetDefaultStream());

            result.Should().NotBeNull();
        }

        [Fact]
        public void ImportExportManager_ImportProductsFromExcel_ShouldReturnDictionary()
        {
            var result = _importExportManager.ImportProductsFromExcel(GetDefaultStream());

            result.Should().HaveCount(0);
        }

        [Fact]
        public void ImportExportManager_ExportProductsToExcel_ShouldReturnByteArray()
        {
            var result = _importExportManager.ExportProductsToExcel();

            result.Should().BeOfType<byte[]>();
        }

        [Fact]
        public void ImportExportManager_ExportProductsToExcel_ShouldCallGetAllOfProductVariantService()
        {
            _importExportManager.ExportProductsToExcel();

            A.CallTo(() => _productVariantService.GetAll()).MustHaveHappened();
        }

        private static Stream GetDefaultStream()
        {
            return new MemoryStream(0);
        }
    }
}