using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApiCoreDocker.Models;
using Xunit;

namespace WebApiCoreDockerTests
{
    public class CustomerTests
    {
        #region PhoneNumber tests

        [Fact]
        public void PhoneNumber_StartingPlus46Following9Digits_IsValid()
        {
            var customer = new Customer
            {
                PhoneNumber = "+46123456789",
            };

            Assert.DoesNotContain(ValidateModel(customer), v => v.MemberNames.Contains("PhoneNumber"));
        }

        [Fact]
        public void PhoneNumber_StartingPlus46Following10Digits_IsNotValid()
        {
            var customer = new Customer
            {
                PhoneNumber = "+461234567890",
            };

            Assert.Contains(ValidateModel(customer), v => v.MemberNames.Contains("PhoneNumber"));
        }

        [Fact]
        public void PhoneNumber_StartingPlus46Following8Digits_IsNotValid()
        {
            var customer = new Customer
            {
                PhoneNumber = "+4612345678",
            };

            Assert.Contains(ValidateModel(customer), v => v.MemberNames.Contains("PhoneNumber"));
        }

        [Fact]
        public void PhoneNumber_NotStartingPlus46_IsNotValid()
        {
            var customer = new Customer
            {
                PhoneNumber = "123456789000",
            };

            Assert.Contains(ValidateModel(customer), v => v.MemberNames.Contains("PhoneNumber"));
        }

        #endregion PhoneNumber tests

        #region EmailAddress tests

        [Fact]
        public void EmailAddress_WitAtAndPoint_IsNotValid()
        {
            var customer = new Customer
            {
                EmailAddress = "test@test.test",
            };

            Assert.DoesNotContain(ValidateModel(customer), v => v.MemberNames.Contains("EmailAddress"));
        }

        [Fact]
        public void EmailAddress_NoAt_IsNotValid()
        {
            var customer = new Customer
            {
                EmailAddress = "testtest.test",
            };

            Assert.Contains(ValidateModel(customer), v => v.MemberNames.Contains("EmailAddress"));
        }

        //[Fact]
        //public void EmailAddress_NoPoint_IsNotValid()
        //{
        //    // This is not validated by EmailAddress dataannotation
        //    var customer = new Customer
        //    {
        //        EmailAddress = "test@testtest",
        //    };

        //    //this is validated by dataannotation with no errors
        //    Assert.DoesNotContain(ValidateModel(customer), v => v.MemberNames.Contains("EmailAddress"));
        //}

        #endregion EmailAddress tests  

        #region SecurityNumber tests  

        [Fact]
        public void SecurityNumber_10Digits_IsValid()
        {
            var customer = new Customer
            {
                SecurityNumber = "1234567890",
            };

            Assert.DoesNotContain(ValidateModel(customer), v => v.MemberNames.Contains("SecurityNumber"));
        }

        [Fact]
        public void SecurityNumber_12Digits_IsValid()
        {
            var customer = new Customer
            {
                SecurityNumber = "123456789012",
            };

            Assert.DoesNotContain(ValidateModel(customer), v => v.MemberNames.Contains("SecurityNumber"));
        }

        [Fact]
        public void SecurityNumber_LessThan10Digits_IsNotValid()
        {
            var customer = new Customer
            {
                SecurityNumber = "12345678",
            };

            Assert.Contains(ValidateModel(customer), v => v.MemberNames.Contains("SecurityNumber"));
        }

        [Fact]
        public void SecurityNumber_MoreThan12Digits_IsNotValid()
        {
            var customer = new Customer
            {
                SecurityNumber = "12345678901234567",
            };

            Assert.Contains(ValidateModel(customer), v => v.MemberNames.Contains("SecurityNumber"));
        }

        [Fact]
        public void SecurityNumber_11Digits_IsNotValid()
        {
            var customer = new Customer
            {
                SecurityNumber = "12345678901",
            };

            Assert.Contains(ValidateModel(customer), v => v.MemberNames.Contains("SecurityNumber"));
        }

        #endregion SecurityNumber tests  

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
