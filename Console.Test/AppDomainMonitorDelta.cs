using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class AppDomainMonitorDelta : IDisposable
    {
        private AppDomain m_appDomain;
        private TimeSpan m_thisADCpu;
        private Int64 m_thisADMemoryInUse;
        private Int64 m_thisADMemoryAllocated;

        static AppDomainMonitorDelta()
        {
            // 打开监控
            AppDomain.MonitoringIsEnabled = true; 
        }
        public AppDomainMonitorDelta(AppDomain ad)
        {
            m_appDomain = ad ?? AppDomain.CurrentDomain;
            m_thisADCpu = m_appDomain.MonitoringTotalProcessorTime;
            m_thisADMemoryInUse = m_appDomain.MonitoringSurvivedMemorySize;
            m_thisADMemoryAllocated = m_appDomain.MonitoringTotalAllocatedMemorySize;
            // 为AppDomain添加异常记录
            m_appDomain.FirstChanceException += M_appDomain_FirstChanceException;
        }
        /// <summary>
        /// 异常记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_appDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
             //TODO: 写日志
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    GC.Collect();
                    Console.WriteLine("FriendlyName ={0}, CPU ={1}ms", m_appDomain.FriendlyName,
                        (m_appDomain.MonitoringTotalProcessorTime - m_thisADCpu).TotalMilliseconds);
                    Console.WriteLine("Allocated {0:N0} bytes of which {1:N0} survived GCs",
                        m_appDomain.MonitoringTotalAllocatedMemorySize - m_thisADMemoryAllocated,
                        m_appDomain.MonitoringSurvivedMemorySize - m_thisADMemoryInUse);
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~AppDomainMonitorDelta() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}
