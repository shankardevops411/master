﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Core.Entities
{
    public class CaregivertasksEntity
    {
        public int CGTASK_ID { get; set; }
        public int? CAREGIVER { get; set; }
        public DateTime PLANNED_DATE { get; set; }
        public DateTime? PLANNED_START_TIME { get; set; }
        public DateTime? PLANNED_END_TIME { get; set; }
        public DateTime? ACCTUAL_DATE { get; set; }
        public DateTime? ACCTUAL_START_TIME { get; set; }
        public DateTime? ACCTUAL_END_TIME { get; set; }
        public string STATUS { get; set; }
        public int? PLAN_ENTRY { get; set; }
        public double? REIMBURSEMENT { get; set; }
        public bool? CONFIRMED { get; set; }
        public string EDITED_HOURS { get; set; }
        public float? MILES { get; set; }
        public int SERVICECODE_ID { get; set; }
        public int CLIENT_ID { get; set; }
        public int? AUTHORIZATION_ID { get; set; }
        public string EDITED_HOURS_PAYABLE { get; set; }
        public double? PAYRATE { get; set; }
        public bool? IS_PAYRATE_HOURLY { get; set; }
        public bool? IS_PAYABLE { get; set; }
        public bool IS_BILLABLE { get; set; }
        public int? SCHEDULE_PROPERTIES { get; set; }
        public string ADDITIONAL_NOTE { get; set; }
        public int? MISSED_VISIT_FLAG { get; set; }
        public int PAYMENT_SOURCE { get; set; }
        public bool? IS_BILLED { get; set; }
        public bool? IS_PAID { get; set; }
        public string MISSED_VISIT_REASON { get; set; }
        public DateTime? WEEK_START { get; set; }
        public float? OFFICE_MILES { get; set; }
        public int? POC { get; set; }
        public int? HHA { get; set; }
        public bool? IS_AUTH_MANDATORY { get; set; }
        public int? RequestId { get; set; }
        public byte? CheckInSource { get; set; }
        public byte? CheckOutSource { get; set; }
        public int? CREATED_BY { get; set; }
        public double? BillRate { get; set; }
        public bool? IS_BILLRATE_HOURLY { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? isPRNVisit { get; set; }
        public int? eChartMasterID { get; set; }
        public bool CanBill { get; set; }
        public char? Chart_Type { get; set; }
        public bool? enablePayableHours { get; set; }
        public bool? CopayBilled { get; set; }
        public bool? isAcceptedFromFC { get; set; }
        public bool? isChartSubmitted { get; set; }
        public bool? isChartApproved { get; set; }
        public bool? isChartSendForCorrection { get; set; }
        public int? oasisDatasetID { get; set; }
        public int? pChartMasterID { get; set; }
        public int? InsertedIndex { get; set; }
        public float? AuthorizedHours { get; set; }
        public string CodingStatus { get; set; }
        public bool? isBilledByContractAgency { get; set; }
        public bool? isPaidToContractAgency { get; set; }
        public float? AuthGraceUnitsApplied { get; set; }
        public bool? AuthGraceUnitsResolved { get; set; }
        public float? TotalHours { get; set; }
        public bool? isScheduleCreatedFromKMobile { get; set; }
        public float? TotalBreakHours { get; set; }
        public string CreateMethod { get; set; }
        public bool? isAuthorized { get; set; }
        public bool? isChartLockedForEditing { get; set; }
        public int? eChartLastLockedSessionId { get; set; }
        public bool? isInterventionTabLockedForEditing { get; set; }
        public int? isInterventionTabLastLockedSessionId { get; set; }
        public int? CaregiverTaskMissedVisitID { get; set; }
        public bool? isOverridePayrate { get; set; }
        public bool? isOverridePayMode { get; set; }
        public bool? IsPartiallyBilled { get; set; }
        public int EvvAggregatorStatus { get; set; }
        public DateTime? EvvStatusLastUpdatedTimeStamp { get; set; }
        public string EvvLastExportStatus { get; set; }
        public DateTime? EvvLastExportDate { get; set; }

        public int CGTaskID { get; set; }
        public string REVENUE_CODE { get; set; }
        public string GCODE { get; set; }
        public double? TotalCharges { get; set; }
        public float? Units { get; set; }
        public string NotesToClinician { get; set; }
        public bool? TimesheetSubmitted { get; set; }
        public bool? ApprovedForPayroll { get; set; }
        public byte? ApprovedForBonusPayRate { get; set; }
        public double? BonusAmount { get; set; }
        public int? PlaceOfService { get; set; }
        public bool? Has_Child_Schedules { get; set; }
        public string Modifier1 { get; set; }
        public string Modifier2 { get; set; }
        public string Modifier3 { get; set; }
        public string Modifier4 { get; set; }
        public int? ShiftID { get; set; }
        public bool? PRNApproved { get; set; }
        public int? PRNApprovedBy { get; set; }
        public DateTime? PRNApprovedOn { get; set; }
        public bool? IsSplitForBilling { get; set; }
        public bool? IsSplitForPayroll { get; set; }
        public double? BaseRate { get; set; }
        public int? CoSignStaff { get; set; }
        public bool? RequireCoSign { get; set; }
        public float? travelTimeHours { get; set; }
        public int? documentationTimeMins { get; set; }
        public byte? updatedToCM2k { get; set; }
        public DateTime UPDATED_TIMESTAMP_FOR_OFFLINE { get; set; }
        public int? ServicePrimarySkill { get; set; }
        public string PayerHours { get; set; }
        public string CancellationReason { get; set; }
        public int? OriginalCgTaskID { get; set; }
        public string CancelledNotes { get; set; }
        public bool? AideDailyTaskApproved { get; set; }
        public DateTime? AideDailyTaskApprovedOn { get; set; }
        public int? AideDailyTaskApprovedBy { get; set; }
        public string EVVVendorUniqueScheduleID { get; set; }
        public bool? PayrollExported { get; set; }
        public string CHECK_IN_LOCATION { get; set; }
        public string CHECK_OUT_LOCATION { get; set; }
        public bool? IsBillRateOverridden { get; set; }
        public bool? daylightSavingsAdjusted { get; set; }
        public int? daylightSavingsAdjustedBy { get; set; }
        public DateTime? daylightSavingsAdjustedOn { get; set; }
        public string MissedVisitNote { get; set; }
        public string UnbilledComment { get; set; }
        public string UnbilledCommentDescription { get; set; }
        public bool? IS_BILLRATE_UnitBased { get; set; }
        public double? Contractual_BillRate { get; set; }
        public bool? isBillHourly_Contractual { get; set; }
        public bool? isUnitBasedBilling_Contractual { get; set; }
        public int? CancelledBy { get; set; }
        public DateTime? CancelledOn { get; set; }
        public string NonBillableReason { get; set; }
        public float? CHECK_IN_DISTANCE { get; set; }
        public float? CHECK_OUT_DISTANCE { get; set; }
        public DateTime? BilledByContractAgencyOn { get; set; }
        public int? BilledByContractAgencyBy { get; set; }
        public DateTime? PaidToContractAgencyOn { get; set; }
        public int? PaidToContractAgencyBy { get; set; }
        public bool? GCodeConfirmed { get; set; }
        public int? evvUploadFailedAttempts { get; set; }
        public bool? IsBillModeOverriden { get; set; }
        public bool? IsContractualRateOverriden { get; set; }
        public bool? IsContractualModeOverriden { get; set; }
        public bool? overrideBonusRules { get; set; }
        public string SERVICE_LOCATION { get; set; }
        public bool? isMileageAutoCalculated { get; set; }
        public DateTime? MileageAutoCalculatedOn { get; set; }
        public bool? requireMileageReCalculation { get; set; }
        public float? AutoCalculatedMiles { get; set; }
        public string HyperTrackActionId { get; set; }
        public string HyperTrackingUrl { get; set; }
        public float? ManualMiles { get; set; }
        public string eChartConcurrencyGUID { get; set; }
        public bool? cosignStaffSetbyKanTime { get; set; }
        public bool? IsTravelTimeAutoCalculated { get; set; }
        public float? AutoCalculatedTravelTimeMins { get; set; }
        public float? ManualTravelTimeMins { get; set; }
        public string DeletedContext { get; set; }
        public string EvvTokenCode_Checkin { get; set; }
        public string EvvTokenCode_CheckinOriginal { get; set; }
        public string EvvTokenCode_Checkout { get; set; }
        public string EvvTokenCode_CheckoutOriginal { get; set; }
        public bool? isRescheduledSchedule { get; set; }
        public int? EvvMyUniqueID { get; set; }
        public bool? isLostRevenue { get; set; }
        public DateTime? TravelTimeAutoCalculatedOn { get; set; }
        public bool isEvvschedule { get; set; }
        public bool isEvvScheduleDirty { get; set; }
        public char EvvCheckinLocationVerified { get; set; }
        public char EvvCheckoutLocationVerified { get; set; }
        public int? CheckinTreatmentLocation { get; set; }
        public int? CheckoutTreatmentLocation { get; set; }
        public byte NotesToCliniciantype { get; set; }
        public DateTime? Receivable_FollowupDate { get; set; }
        public bool isAttested { get; set; }
        public int? AttestedBy { get; set; }
        public DateTime? AttestedOn { get; set; }
        public bool? IsGeolocationallowedbyUser { get; set; }
        public bool isEvvAggregatorExportRequired { get; set; }
        public bool isVisitVerifiedWithEvvAggregator { get; set; }

        public int CgTaskID { get; set; }
        public int? SMS_ScheduleAlertSentStatus { get; set; }
        public DateTime? SMS_ScheduleAlertSentOn { get; set; }
        public DateTime? SMS_ScheduleAlertSentStatusLastUpdatedTime { get; set; }
        public int? SMS_SchedeuleLateCheckinStatus { get; set; }
        public DateTime? SMS_SchedeuleLateCheckinSentOn { get; set; }
        public DateTime? SMS_SchedeuleLateCheckinStatusLastUpdatedTime { get; set; }
        public bool? patientUnableToDigitalSign { get; set; }
        public string patientUnableToDigitalSignReason { get; set; }
        public string patientUnableToDigitalSignNotes { get; set; }
        public bool? SchedulesOver24HrDayReviewed { get; set; }
        public int? SchedulesOver24HrDayReviewedBy { get; set; }
        public DateTime? SchedulesOver24HrDayReviewedOn { get; set; }
        public bool? HomeboundStatus { get; set; }
        public string AdditionalHCPCSAndModifiers { get; set; }
        public bool? isHCPCSModified { get; set; }
        public bool? isRevenueModified { get; set; }
        public DateTime? ClinicianConfirmedOn { get; set; }
        public int? ClinicianConfirmedBy { get; set; }
        public bool? isDirtyCalculatedColumns { get; set; }
        public double? Calculated_BilledAmount { get; set; }
        public double? Calculated_ContractualAmount { get; set; }
        public double? Calculated_PaidAmount { get; set; }
        public double? Calculated_PayrolledAmount { get; set; }
        public double? Calculated_BillRate { get; set; }
        public double? Calculated_ContractualRate { get; set; }
        public double? Calculated_Payrate { get; set; }
        public bool? Calculated_BillModeHourly { get; set; }
        public bool? Calculated_BillModeUnitRate { get; set; }
        public bool? Calculated_ContractualModeHourly { get; set; }
        public bool? Calculated_ContractualModeUnitRate { get; set; }
        public bool? Calculated_PayModeHourly { get; set; }
        public double? Calculated_BaseRate { get; set; }
        public int? Calculated_BaseRateHours { get; set; }
        public bool? Calculated_BaseRateEnabled { get; set; }
        public DateTime? LastsentForCorrectionOn { get; set; }
        public int? LastsentForCorrectionBy { get; set; }
        public bool? OverrideMileageRate { get; set; }
        public double? MileageRate { get; set; }
        public int? SMS_SchedeuleEarlyCheckOutStatus { get; set; }
        public DateTime? SMS_SchedeuleEarlyCheckOutSentOn { get; set; }
        public DateTime? SMS_SchedeuleEarlyCheckOutStatusLastUpdatedTime { get; set; }
        public bool? eChartLanguageInterpreterUsed { get; set; }
        public string eChartLanguageInterpreter_Language { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public DateTime? EVVOriginalCheckinTime { get; set; }
        public DateTime? EVVOriginalCheckoutTime { get; set; }
        public string CheckinEditReason { get; set; }
        public string CheckoutEditReason { get; set; }
        public bool? SupervisoryVisitPerformedInThisVisit { get; set; }
        public DateTime? InitialsentforCorrection { get; set; }
        public int? Notifyd_ScheduleAlertSentStatus { get; set; }
        public DateTime? Notifyd_ScheduleAlertSentOn { get; set; }
        public DateTime? Notifyd_ScheduleAlertSentStatusLastUpdatedTime { get; set; }
        public int? Notifyd_SchedeuleLateCheckinStatus { get; set; }
        public DateTime? Notifyd_SchedeuleLateCheckinSentOn { get; set; }
        public DateTime? Notifyd_SchedeuleLateCheckinStatusLastUpdatedTime { get; set; }
        public int? Notifyd_SchedeuleEarlyCheckOutStatus { get; set; }
        public DateTime? Notifyd_SchedeuleEarlyCheckOutSentOn { get; set; }
        public DateTime? Notifyd_SchedeuleEarlyCheckOutStatusLastUpdatedTime { get; set; }
        public int? CdsPlanYear { get; set; }
        public int? CdsPlanYearService { get; set; }
        public int? CDSClientBudgetLineItemID { get; set; }
        public string SplitPurpose { get; set; }
        public bool? IsEVVExported { get; set; }
        public bool? IsEVVDirty { get; set; }
        public string CDSPayRateEditComments { get; set; }
        public string CDSOverrideWeekHoursRulesComments { get; set; }
        public bool? IsCDSTaxBilledUnitMode { get; set; }
        public string CheckinEditCode { get; set; }
        public string CheckoutEditCode { get; set; }
        public string CheckinEditAction { get; set; }
        public string CheckoutEditAction { get; set; }
        public string CheckinEditActionCode { get; set; }
        public string CheckoutEditActionCode { get; set; }
        public bool? Calculated_PayModeHourly_Prev { get; set; }
        public double? Calculated_Payrate_Prev { get; set; }
        public string AssessmentNotes { get; set; }
        public double? Calculated_MileageRate_Prev { get; set; }
        public double? Calculated_MileageRate { get; set; }
        public string EvvOutsideCheckInLocationReason { get; set; }
        public string EvvOutsideCheckOutLocationReason { get; set; }
        public string EvvOutsideCheckInLocationReasonCode { get; set; }
        public string EvvOutsideCheckOutLocationReasonCode { get; set; }
        public string EvvOutsideCheckInLocationNotes { get; set; }
        public string EvvOutsideCheckOutLocationNotes { get; set; }
        public string EvvCheckInVoicePath { get; set; }
        public string EvvCheckOutVoicePath { get; set; }
        public string OverHoursNotes { get; set; }
        public int? CheckinSource_Online { get; set; }
        public int? CheckOutSource_Online { get; set; }
        public DateTime? OriginalCheckinTimeUTC { get; set; }
        public DateTime? OriginalCheckoutTimeUTC { get; set; }
    }
}
