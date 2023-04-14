using System.Threading.Tasks;
using Xamarin.Forms;

namespace PulseOximeterApp.Infrastructure.DependencyServices
{
    public class ShowMessageDependencyService : IShowMessageDependencyService
    {
        public async Task ShowAlertAsync(string message)
        {
            await Shell.Current.DisplayAlert("Внимание", message, "Ок");
        }

        public async Task<bool> ShowQuestionAsync(string title, string message)
        {
            return await Shell.Current.DisplayAlert(title , message, "Да", "Нет");
        }
    }
}
