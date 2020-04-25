using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SiteSec.Models.Scheduler
{
    public class SchedulerMeetingService : ISchedulerEventService<MeetingViewModel>
    {

        readonly Api api = new Api();

        public void Delete(MeetingViewModel appointment, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }


        public IList<MeetingViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<MeetingViewModel> GetAllAsync()
        {
            bool IsWebApiRequest = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
            IList<MeetingViewModel> result = null;

            if (!IsWebApiRequest)
            {
                result = HttpContext.Current.Session["SchedulerTasks"] as IList<MeetingViewModel>;
            }

            if (result == null)
            {

                var apiRetorno = api.Use(HttpMethod.Get, new MeetingViewModel(), $"api/Empresa").Result;
                var str = JsonConvert.SerializeObject(apiRetorno.result);
                var obj = JsonConvert.DeserializeObject<List<MeetingViewModel>>(str);

                result = obj.ToList().Select(task => new MeetingViewModel
                {
                    TaskID = task.TaskID,
                    Title = task.Title,
                    Start = DateTime.SpecifyKind(task.Start, DateTimeKind.Utc),
                    End = DateTime.SpecifyKind(task.End, DateTimeKind.Utc),
                    StartTimezone = task.StartTimezone,
                    EndTimezone = task.EndTimezone,
                    Description = task.Description,
                    IsAllDay = task.IsAllDay,
                    RecurrenceRule = task.RecurrenceRule,
                    RecurrenceException = task.RecurrenceException,
                    RecurrenceID = task.RecurrenceID,
                    OwnerID = task.OwnerID
                }).ToList();

                if (!IsWebApiRequest)
                {
                    HttpContext.Current.Session["SchedulerTasks"] = result;
                }
            }

            return result;
        }

        public void Insert(MeetingViewModel appointment, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }

        public void Update(MeetingViewModel appointment, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar chamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: descartar estado gerenciado (objetos gerenciados).
                }

                // TODO: liberar recursos não gerenciados (objetos não gerenciados) e substituir um finalizador abaixo.
                // TODO: definir campos grandes como nulos.

                disposedValue = true;
            }
        }

        // TODO: substituir um finalizador somente se Dispose(bool disposing) acima tiver o código para liberar recursos não gerenciados.
        // ~SchedulerMeetingService()
        // {
        //   // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
        //   Dispose(false);
        // }

        // Código adicionado para implementar corretamente o padrão descartável.
        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
            Dispose(true);
            // TODO: remover marca de comentário da linha a seguir se o finalizador for substituído acima.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}