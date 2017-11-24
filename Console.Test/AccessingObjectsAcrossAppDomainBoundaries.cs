using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    /// <summary>
    /// 跨越AppDomain边界访问对象
    /// </summary>
    public class AccessingObjectsAcrossAppDomainBoundaries
    {
        public static void Marshalling()
        {
            // 获取AppDomain引用（调用线程当前正在该A品牌Domain中执行）
            AppDomain adCallingThreadDomain = Thread.GetDomain();
            // 获取这个AppDomain的友好字符串名称并显示它
            String callingDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine("Default AppDomain's friendly name={0}", callingDomainName);
            // 获取并显示我们的AppDomain中包含了Main方法的程序集
            String exeAssembly = System.Reflection.Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Main assembly={0}", exeAssembly);

            // 定义局部变量来引用一个AppDomain
            AppDomain ad2 = null;
            // demo1 使用按引用封装进行跨AppDomain通信
            Console.WriteLine("{0}Dome #1", Environment.NewLine);
            //新建一个AppDomain(从当前AppDomain继承安全性和配置)
            ad2 = AppDomain.CreateDomain("AD #1", null, null);
            MarshalByRefType mbrt = null;// 定义 Marshal-by-reference

            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, "ConsoleTest.MarshalByRefType");

            Console.WriteLine("Type={0}", mbrt.GetType());

            Console.WriteLine();


            Console.WriteLine("Is proxy={0}", System.Runtime.Remoting.RemotingServices.IsTransparentProxy(mbrt));

            //看起来像是在MarshalByRefType上调用一个方法，实则不然
            //我们是在代理类型上调用一个方法，代理使线程切换到拥有的对象的
            //那个AppDomain，并在真实的对象上调用这个方法
            mbrt.SomeMethod();

            // 卸载新的AppDomain
            AppDomain.Unload(ad2);

            // mbrt引用一个有效的代理对象，代理对象引用一个无效的AppDomain
            try
            {
                // 
                mbrt.SomeMethod();
                Console.WriteLine("Successfull call.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed call. " + ex.ToString());
            }
        }

        public static void Marshalling2()
        {
            // 获取AppDomain引用（调用线程当前正在该A品牌Domain中执行）
            AppDomain adCallingThreadDomain = Thread.GetDomain();
            // 获取这个AppDomain的友好字符串名称并显示它
            String callingDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine("Default AppDomain's friendly name={0}", callingDomainName);
            // 获取并显示我们的AppDomain中包含了Main方法的程序集
            String exeAssembly = System.Reflection.Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Main assembly={0}", exeAssembly);

            // 定义局部变量来引用一个AppDomain
            AppDomain ad2 = null;

            // Demo 2 :使用按值封送（Marshal-by-Value）进行跨AppDomain通信
            Console.WriteLine("{0}Demo #2 ", Environment.NewLine);
            // 新建一个appDomain（从当前AppDomain继承安全性和配置）
            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            MarshalByRefType mbrt = null;// 定义 Marshal 
            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, "ConsoleTest.MarshalByRefType");
            //对象的方法返回 对象的副本
            // 对象按值（而非按引用）封送
            MarshalByValType mbvt = mbrt.MethodWithReturn();
            // 证明得到的是对一个代理对象的引用
            Console.Write("证明得到的是对一个代理对象的引用 ");
            Console.WriteLine("Is proxy={0} ", RemotingServices.IsTransparentProxy(mbrt));
            // 证明得到的不是对一个代理对象的引用
            Console.Write("证明得到的不是对一个代理对象的引用 ");
            Console.WriteLine("Is proxy={0} ", RemotingServices.IsTransparentProxy(mbvt));
            //在MarshalByValType上调用一个方法
            Console.WriteLine("Returned object created " + mbvt.ToString());

            // 卸载
            AppDomain.Unload(ad2);

            try
            {
                Console.WriteLine("Returned object created " + mbvt.ToString());
                Console.WriteLine("Successfull call. ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed call. " + ex.ToString());
            }

        }

        public static void Marshalling3()
        {
            // 获取AppDomain引用（调用线程当前正在该A品牌Domain中执行）
            AppDomain adCallingThreadDomain = Thread.GetDomain();
            // 获取这个AppDomain的友好字符串名称并显示它
            String callingDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine("Default AppDomain's friendly name={0}", callingDomainName);
            // 获取并显示我们的AppDomain中包含了Main方法的程序集
            String exeAssembly = System.Reflection.Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Main assembly={0}", exeAssembly);

            // 定义局部变量来引用一个AppDomain
            AppDomain ad2 = null;

            // Demo 2 :使用不可封送的类型进行跨AppDomain通信
            Console.WriteLine("{0}Demo #3 ", Environment.NewLine);
            // 新建一个appDomain（从当前AppDomain继承安全性和配置）
            ad2 = AppDomain.CreateDomain("AD #3", null, null);
            MarshalByRefType mbrt = null;// 定义 Marshal 
            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, "ConsoleTest.MarshalByRefType");



            try
            {
                //对象的方法返回一个不可封送的对象：抛出异常
                NonMarshalableType nmt = mbrt.MethodArgAndReturn(callingDomainName);
                // 证明得到的是对一个代理对象的引用
                Console.Write("证明得到的是对一个代理对象的引用 ");
                Console.WriteLine("Is proxy={0} ", RemotingServices.IsTransparentProxy(mbrt));
                Console.WriteLine("Returned object created " + nmt.ToString());
                Console.WriteLine("Successfull call. ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed call. " + ex.ToString());
            }
            // 卸载
            // 一旦调用Unload，只有它返回之后，线程才能恢复运行  （阻塞！）
            AppDomain.Unload(ad2);

        }

    }

    public sealed class MarshalByRefType : MarshalByRefObject
    {
        public MarshalByRefType()
        {
            Console.WriteLine("{0} ctor Running {1} CurrentDomain ={2}", this.GetType().ToString()
                , Thread.GetDomain().FriendlyName, System.AppDomain.CurrentDomain);
            
        }

        public void SomeMethod()
        {
            Console.WriteLine("Executing SomeMethod " + Thread.GetDomain().FriendlyName + "System.AppDomain.CurrentDomain=" + System.AppDomain.CurrentDomain);
        }
        public MarshalByValType MethodWithReturn()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName+ "System.AppDomain.CurrentDomain="+ System.AppDomain.CurrentDomain);
            MarshalByValType t = new MarshalByValType();
            return t;
        }
        public NonMarshalableType MethodArgAndReturn(string callingDomainName)
        {
            Console.WriteLine("Calling from {0} to {1}. {2} ", callingDomainName
                , Thread.GetDomain().FriendlyName, System.AppDomain.CurrentDomain);
            NonMarshalableType t = new NonMarshalableType();
            return t;
        }
        /// <summary>
        /// 默认的5分钟和2分钟租约期设定修改在此处
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return base.InitializeLifetimeService();
        }
    }
    // 该类的实例可跨越appDomain的边界“按值封送”
    [Serializable]
    public sealed class MarshalByValType : Object
    {
        private DateTime m_creationTime = DateTime.Now;
        public MarshalByValType()
        {
            Console.WriteLine("{0} ctor running in {1}, Created on {2:D} System.AppDomain.CurrentDomain={3}",
                this.GetType().ToString(), Thread.GetDomain().FriendlyName,
                m_creationTime, System.AppDomain.CurrentDomain);
        }
        public override string ToString()
        {
            return m_creationTime.ToLongDateString();
        }
    }
    // 该类的实例不能跨appDomain边界进行封装(必须有序列化标记)
    //  [Serializable]
    public sealed class NonMarshalableType : Object
    {
        public NonMarshalableType()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName+ "System.AppDomain.CurrentDomain="+ System.AppDomain.CurrentDomain);
        }
    }

}
