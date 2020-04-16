using GamerHub.mobile.core.Models.Db;

namespace GamerHub.mobile.core.Services.Db
{
    public interface ISqlLiteService
    {
        UserCredentialsModel GetCredentialsStoredInDb();
        void SaveCredentials(UserCredentialsModel model);
        void ClearCredentials();
    }
}
