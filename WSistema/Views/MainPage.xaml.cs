using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;
using WSistema.Models;

namespace WSistema.Views;

public partial class MainPage : ContentPage
{
    private string _deviceToken;
    PrevisaoResponse PrevisaoResponse = new PrevisaoResponse();
    public MainPage()
    {
        InitializeComponent();

        BindingContext = App.Previsao;

        if (Preferences.ContainsKey("DeviceToken"))
        {
            _deviceToken = Preferences.Get("DeviceToken", "");
        }

    }
    private async void MandarNotificao(int previsao)
    {
        var title = "";
        var body = "";

        switch (previsao)
        {
            case 1:

                title = "RISCO MODERADO NA SUA REGIÃO";
                body = "Há risco moderado de chuva na sua região";
                break;

            case 2:

                title = "RISCOS INTESO DE CHUVA NA SUA REGIÃO";
                body = "Há risco inteso de chuva na sua região";
                break;

            case 3:

                title = "CUIDADO HÁ RISCO MUITO ALTO DE CHUVA NÁ SUA REGIÃO";
                body = "Há risco muito intenso de chuva na sua região";
                break;


            default:
                break;
        }

        var androidNotificationObject = new Dictionary<string, string>();
        var pushNotificationRequest = new PushNotificationRequest
        {
            notification = new NotificationMessageBody
            {
                title = title,
                body = body
            },
            data = androidNotificationObject,
            registration_ids = new List<string> { _deviceToken }
        };

        string url = "https://fcm.googleapis.com/fcm/send";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", "=" + "AAAADWxRPKc:APA91bHHDW2r5UxC8t4Z-yjYHdD9PEVMJhT-4ZNv4K74z4SCPHwrgn6amDNOFqP6Yopvhe_-xw447eZM9cl4dNd2hN_sUfVyfnN9D1mywFfzF7_dcDb8Yg-IxaKDA9jdF4-9DKswg9VD");

            string serializeRequest = JsonConvert.SerializeObject(pushNotificationRequest);
            var response = await client.PostAsync(url, new StringContent(serializeRequest, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert(title, body, "OK");
            }
        }
    }

    protected override void OnParentChanging(ParentChangingEventArgs args)
    {
        base.OnParentChanging(args);
        var shellContent = args.NewParent as ShellContent;
        var route = shellContent.Route;
        shellContent.Title = "Previsão do tempo";

        switch (route)
        {
            case "StMonicaHj":
                this.GetPrevisao(1);
                break;

            case "EducaHj":
                this.GetPrevisao(3);
                break;

            case "GloriaHj":
                this.GetPrevisao(4);
                break;

            case "MtCarmeloHj":
                this.GetPrevisao(5);
                break;

            case "PatosHj":
                this.GetPrevisao(7);
                break;

            case "PontalHj":
                this.GetPrevisao(6);
                break;

            case "UmuHj":
                this.GetPrevisao(2);
                break;

            case "StMonicaAmanha":
                this.GetPrevisao(1, false);
                break;

            case "EducaAmanha":
                this.GetPrevisao(3, false);
                break;

            case "GloriaAmanha":
                this.GetPrevisao(4, false);
                break;

            case "MtCarmeloAmanha":
                this.GetPrevisao(5, false);
                break;

            case "PatosAmanha":
                
                this.GetPrevisao(7, false);
                break;

            case "PontalAmanha":
                this.GetPrevisao(6, false);
                break;

            case "UmuAmanha":
                shellContent.Title = "Umuarama";
                this.GetPrevisao(2, false);
                break;


            default: break;
        }
    }


    private void GetPrevisao(int id, bool hoje = true)
    {
        if (hoje)
        {
            if (App.ListPrevisao.PrevisaoAtual.FirstOrDefault(x => x.id == id) != null)
            {
                App.Previsao = App.ListPrevisao.PrevisaoAtual.FirstOrDefault(x => x.id == id);
                BindingContext = App.Previsao;
            }
        }

        else
        {
            if (App.ListPrevisao.PrevisaoAmanha.FirstOrDefault(x => x.id == id) != null) {
                App.Previsao = App.ListPrevisao.PrevisaoAmanha.FirstOrDefault(x => x.id == id);
                BindingContext = App.Previsao;
            }
        }
    }

}