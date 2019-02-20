using DataAcess.Crud;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Combiner
{
    public class ExceptionManager
    {
        public string PATH = @"C:\Users\Universidad Cenfotec\Desktop\Logs";
        private static ExceptionManager instance;
        private static Dictionary<int, ApplicationMessage> messages = new Dictionary<int, ApplicationMessage>();

        private ExceptionManager()
        {
            LoadMessages();
        }

        public static ExceptionManager GetInstance()
        {
            if (instance == null)
                instance = new ExceptionManager();

            return instance;
        }

        public void Process(Exception ex)
        {
            var bex = new BusinessException();

            if (bex.GetType() == typeof(BusinessException))
            {
                bex = (BusinessException)ex;
            }
            else
            {
                bex = new BusinessException(0, ex);
            }

            ProcessBusinessException(bex);
        }

        private void ProcessBusinessException(BusinessException bex)
        {
            var today = DateTime.Now.ToString("YYYYmmdd");
            var logName = PATH + today + "_" + "log.txt";
            var message = bex.Message + "\n" + bex.StackTrace + "n";

            if (bex.InnerException != null)
                message += bex.InnerException.Message + "\n" + bex.InnerException.StackTrace;

            using (StreamWriter w = File.AppendText(logName))
            {
                Log(bex.Message, w);
            }

            bex.AppMessage = GetMessage(bex);
            throw bex;
        }

        private ApplicationMessage GetMessage(BusinessException bex)
        {
            var appMessage = new ApplicationMessage { MESSAGE = "Message not found!" };

            if (messages.ContainsKey(bex.ExceptionId))
                appMessage = messages[bex.ExceptionId];

            return appMessage;
        }

        private void Log(string message, StreamWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", message);
            w.WriteLine("-------------------------------");
        }

        private void LoadMessages()
        {
            var crudMessages = new AppMessagesCrudFactory();
            var lstMessages = crudMessages.RetrieveAll<ApplicationMessage>();

            foreach (var appMessage in lstMessages)
            {
                messages.Add(appMessage.ID, appMessage);
            }
        }
    }
}

