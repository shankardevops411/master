 using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;   
using Scheduleservice.Services.Schedules.Query;
using Scheduleservice.Services.Schedules.Command;
using System;

namespace Scheduleservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleEVVController : ControllerBase
    {
        private IMediator _mediator;
        ILogger<ScheduleEVVController> _logger;

        public ScheduleEVVController(IMediator mediator, ILogger<ScheduleEVVController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/Schedule
        [Route("evv/batch")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ScheduleEVVBatchQuery scheduleEVVBatchQuery)
        {
            try
            { 
                _logger.LogInformation("----- Get ScheduleEVVBatchQuery");

                var result = await _mediator.Send(scheduleEVVBatchQuery);
                return Ok(result);
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex.ToString()); 
                return BadRequest(ex);
            }

           
        }

        [Route("evv/batch")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]  ScheduleEVVBatchCommand scheduleEVVBatchCommand)
        {
            try
            {

            
            _logger.LogInformation("----- Update ScheduleEVVBatch");
            var result = await _mediator.Send(scheduleEVVBatchCommand);
            return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex);
            }
        }

        // GET: api/ScheduleEVV
        //[Route("evv/InconsistencySchedules")]
        //[HttpGet]
        //public async Task<IActionResult> GetEVVFlagInconsistencies([FromQuery] ScheduleEVVQuery _ScheduleEVVQuery)
        //{
        //    // Task<Response<IEnumerable<ScheduleEVVInfoDto>>>
        //    _logger.LogInformation("----- Get ScheduleEVVController GetEVVFlagInconsistencies");
        //    // var records = await _mediator.Send(_ScheduleEVVQuery);

        //    var records = "";
        //    //var workbook = new XLWorkbook();
        //    //workbook.AddWorksheet("sheetName");
        //    //var ws = workbook.Worksheet("sheetName");

        //    //int row = 1;
        //    //foreach (var item in records.ToList())
        //    //{
        //    //    ws.Cell("A" + row.ToString()).Value = item.CGTASK_ID.ToString();
        //    //    // ws.Cell("A" + row.ToString()).Value = item.ToString();
        //    //    row++;
        //    //}
        //    //foreach (var item in records.ToList())
        //    //{
        //    //    ws.Cell("A" + row.ToString()).Value = item.CGTASK_ID.ToString();
        //    //   // ws.Cell("A" + row.ToString()).Value = item.ToString();
        //    //    row++;
        //    //}

        //    //workbook.SaveAs("yourExcel.xlsx");
        //    return Ok(records);
        //}

        //[HttpPut]
        //[Route("evv/IsEVVScheduleFlag")]
        //public async Task<IActionResult> UpdateIsEVVScheduleFlag([FromQuery] UpdateIsEVVscheduleCommand _UpdateIsEVVscheduleCommand)
        //{
        //    _logger.LogInformation("----- Post ScheduleEVVController UpdateIsEVVScheduleFlag");
        //    //  var records = await _mediator.Send(_UpdateIsEVVscheduleCommand);
        //    var records = "";
        //    return Ok(records);
        //}


        [HttpGet]
        [Route("evv/GetSchedulesByFilterJSON")]
        public async Task<IActionResult> GetSchedulesByFilterJSON([FromQuery] GetSchedulesByFilterJsonQuery _GetSchedulesByFilterJsonCommand)
        {
            try
            {
                _logger.LogInformation("----- Get ScheduleEVVController GetSchedulesByFilterJSON ");
                var records = await _mediator.Send(_GetSchedulesByFilterJsonCommand);
                return Ok(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex);
            }
        }

    }
}
