using System;
using System.Collections.Generic;
using System.Text;
using WallProject.Models;
using Xunit;

namespace WallProjectTest
{
   public class IdCoderTest
    {

        [Theory]
        [InlineData(1,0)]
        [InlineData(2, 1)]
        public void Test_CreateFrontId(int id,int frontId)
        {
            Assert.Equal(IdCoder.CreateFrontId(id), frontId);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        public void Test_GetOrginalId(int id)
        {
            int frontId=IdCoder.CreateFrontId(id);
            Assert.Equal(IdCoder.GetOrginalId(frontId), id);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        public void Test_GetFrontId(int id)
        {
            int frontId = IdCoder.CreateFrontId(id);
            Assert.Equal(IdCoder.GetFrontId(id),frontId);
        }

    }
}
