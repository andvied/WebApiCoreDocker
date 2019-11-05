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

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
