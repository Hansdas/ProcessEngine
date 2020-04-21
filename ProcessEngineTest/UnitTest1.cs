using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProcessEngine.EF;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProcessEngineTest
{
    public class UnitTest1
    {
        public static DbContextOptions<DBContext> CreateDbContextOptions()
        {
            var serviceProvider = new ServiceCollection().
                AddDbContext<DBContext>()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<DBContext>();
            builder.UseMySql("Data Source=58.87.92.221;Database=Blog;User ID=sa;Password=Sa@123456;pooling=true;port=3306;sslmode=none;CharSet=utf8");
            return builder.Options;
        }
        [Fact]
        public void Test1()
        {
            //var option= CreateDbContextOptions();
            // DBContext _dBContext = new DBContext(option);
            // var v= _dBContext.Model.FindEntityType(typeof(WorkFlowNode).FullName);
            // var list = v.GetProperties();
            string s = "{\"fa490c67-bdb2-4ddd-810d-894e9ceeba0e\":{\"top\":191,\"left\":55,\"process_to\":[\"bd7c788d-9d2f-48da-80ad-726edb8dffb1\"]},\"bd7c788d-9d2f-48da-80ad-726edb8dffb1\":{\"top\":171,\"left\":281,\"process_to\":[]}}";
            dynamic dynamics = JsonConvert.DeserializeObject<dynamic>(s);
            for(int i = 0; i < dynamics.Count; i++)
            {
                dynamic ss = dynamics["bd7c788d-9d2f-48da-80ad-726edb8dffb1"];
            }



        }
    }
}
