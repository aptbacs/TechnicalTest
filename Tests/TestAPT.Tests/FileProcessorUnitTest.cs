using System;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using TestAPT.Interfaces;
using TestAPT.Models;
using TestAPT.Validators;
using Xunit;
using TestAPT.Processors;

namespace TestAPT.Tests
{
    public class FileProcessorUnitTest
    {
        FileDetailValidator detailValidator;
        public FileProcessorUnitTest()
        {
            //Setup
            this.detailValidator = new FileDetailValidator();
        }
        [Fact]
        public void Test_Valid_Argument()
        {
            var fileProcessor = new Mock<IProcessor>();
            fileProcessor.Setup(p => p.ProcessFile(It.IsAny<IWebHostEnvironment>(), 
                It.IsAny<IFormFile>())).Returns(Task.FromResult(new YieldResult()));
            Assert.True(fileProcessor.Object != null);
        }

        [Fact]
        public void Test_Invalid_Amount_FileDetail()
        {
            var d = GetFileDetailWithoutAmount();
            var ex = detailValidator.Validate(d);
            Assert.False(ex.IsValid);
        }

        [Fact]
        public void Test_FileDetail_Amount_LargerThanMaximum()
        {
            const string msg = "[Amount] cannot be greater that 20,000,000 !";
            var d = GetFileDetail();
            d.Amount = 30000000m;
            var ex = detailValidator.Validate(d);
            Assert.False(ex.IsValid);
            Assert.Equal(msg, ex.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Test_FileDetail_Amount_LowerThanMinimum()
        {
            const string errorMsg = "[Amount] must be greather than or equal to 1.00 No Less !";
;
            var d = GetFileDetail();
            d.Amount = -5630.00m;
            var ex = detailValidator.Validate(d);
            Assert.False(ex.IsValid);
            Assert.Equal(errorMsg, ex.Errors[0].ErrorMessage);
        }

        private FileDetail GetFileDetail()
        {
            return new FileDetail
            {
                Code = "ABBAHBBZ",
                Name = "Frazzle Dazzle",
                Reference = "OTT-1234",
                Amount = 245.50m
            };
        }

        private FileDetail GetFileDetailWithoutAmount()
        {
            return new FileDetail
            {
                Code = "ABBAHBOO",
                Name = "Johnny Whalehandler",
                Reference = "BBC-5678"
            };
        }

        
    }
}
