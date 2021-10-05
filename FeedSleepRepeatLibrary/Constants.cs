using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedSleepRepeatLibrary
{
    public class Constants
    {
        public const string FeedNotAddedNoBabySelected = "Feed could not be added because a baby hasn't been selected.";
        public const string SleepNotAddedNoBabySelected = "Sleep could not be added because a baby hasn't been selected.";

        public const string CreationFailedBabyAlreadyExists = "Creation was unsuccessful because a baby with this name already exists.";
        public const string CreationFailedInvalidNameFormat = "Creation was unsuccessful because babies must have exactly one first name and one last name.";

        public const string UpdateFailedBabyNotCreated = "This baby couldn't be updated because it hasn't been created yet.";
        public const string UpdateFailedBabyNotSelected = "Updating was unsuccessful because a baby hasn't been selected.";

        public const string DeleteFailedBabyNotCreated = "This baby couldn't be deleted because it hasn't been created yet.";
        public const string DeleteFailedBabyNotSelected = "Deleting was unsuccessful because a baby hasn't been selected.";
        public const string DeleteBabyYesNo = "Are you sure you want to permanently delete this baby's record?";
        public const string DeleteBabyCaption = "Delete Confirmation";

        public const string DefaultAge = "0y 0m 0d";
    }
}
