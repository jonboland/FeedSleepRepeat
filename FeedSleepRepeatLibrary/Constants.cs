namespace FeedSleepRepeatLibrary
{
    public class Constants
    {
        public const string DataFolder = "FSR";
        public const string FatalErrorOccured = "Sorry for the disruption.\n\nUnfortunately, FeedSleepRepeat has stopped working because the following fatal error occured:\n\n";
        public const string FatalErrorCaption = "Fatal Error";
        public const string DatabaseName = "FeedSleepRepeatDB.db";
        public const string AppStart = "Application started.";
        public const string AppClosure = "Application closed.";

        public const string FeedNotAddedNoBabySelected = "Feed could not be added because a baby hasn't been selected.";
        public const string SleepNotAddedNoBabySelected = "Sleep could not be added because a baby hasn't been selected.";

        public const string ChangeBabyYesNo = "Changes will be lost if you select a different baby without updating/creating.\n\nDo you wish to proceed?";
        public const string ChangeBabyCaption = "Change Baby Confirmation";

        public const string ChangeDateYesNo = "Changes will be lost if you select a different day without updating/creating.\n\nDo you wish to proceed?";
        public const string ChangeDateCaption = "Change Date Confirmation";

        public const string CreationFailedBabyAlreadyExists = "Creation was unsuccessful because a baby with this name already exists.";
        public const string CreationFailedInvalidNameFormat = "Creation was unsuccessful because babies must have exactly one first name and one last name.";

        public const string UpdateFailedBabyNotCreated = "This baby couldn't be updated because it hasn't been created yet.";
        public const string UpdateFailedBabyNotSelected = "Updating was unsuccessful because a baby hasn't been selected.";
        public const string UpdateFailedWeightNotValid = "Updating was unsuccessful because the weight field contains one or more characters that are not digits.";

        public const string DeleteFailedBabyNotCreated = "This baby couldn't be deleted because it hasn't been created yet.";
        public const string DeleteFailedBabyNotSelected = "Deleting was unsuccessful because a baby hasn't been selected.";
        public const string DeleteBabyYesNo = "Are you sure you want to permanently delete this baby's record?";
        public const string DeleteBabyCaption = "Delete Confirmation";

        public const string FormCloseYesNoUpdate = "Are you sure want to exit without updating this baby?\n\nRecent changes will be lost.";
        public const string FormCloseYesNoCreate = "Are you sure want to exit without creating this baby?";
        public const string FormCloseCaption = "Exit";

        public const string DefaultAge = "0y 0m 0d";
    }
}
