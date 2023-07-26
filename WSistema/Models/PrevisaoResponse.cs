using JetBrains.Annotations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WSistema.Models
{
    public class PrevisaoResponse : INotifyPropertyChanged
    {
        public PrevisaoResponse()
        {
            id = 0;
            intensidade = 0;
            precipitacao = 0;
            temp_max = 0;
            temp_min = 0;
            temp_med = 0;
            humidade = 0;
        }
        public int _id { get; set; }
        public int _intensidade { get; set; }
        public double _precipitacao { get; set; }
        public double _temp_max { get; set; }
        public double _temp_min { get; set; }
        public double _temp_med { get; set; }
        public double _humidade { get; set; }

        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("id");
            }
        }

        public string title
        {

            get { return this.GetTitle(); }

        }

        public int intensidade
        {
            get
            {
                return _intensidade;
            }
            set
            {
                _intensidade = value;
                OnPropertyChanged("intesidade");
            }
        }
        public double precipitacao
        {
            get
            {
                return _precipitacao;
            }
            set
            {
                _precipitacao = value;
                OnPropertyChanged("precipitacao");
            }
        }
        public double temp_max
        {
            get
            {
                return _temp_max;
            }
            set
            {
                _temp_max = value;
                OnPropertyChanged("temp_max");
            }
        }
        public double temp_min
        {
            get
            {
                return _temp_min;
            }
            set
            {
                _temp_min = value;
                OnPropertyChanged("temp_min");
            }
        }
        public double temp_med
        {
            get
            {
                return _temp_med;
            }
            set
            {
                _temp_med = value;
                OnPropertyChanged("temp_med");
            }
        }
        public double humidade
        {
            get
            {
                return _humidade;
            }
            set
            {
                _humidade = value;
                OnPropertyChanged("humidade");
            }
        }

        public string intensidadeStr
        {

            get { return this.intensidadeString(); }
        }

        public string intensidadeString()
        {
            switch (this.intensidade)
            {
                case 0:
                    return "Sem Chuva";
                case 1:
                    return "Pouca Chuva";
                case 2:
                    return "Chuva Moderada";
                case 3:
                    return "Chuva Intensa";

                default: return null;
            }
        }

        public string GetTitle()
        {
            switch (this.id)
            {
                case 1: return "Santa Mônica";
                case 2: return "Umuarama";
                case 3: return "Educação Física";
                case 4: return "Glória";
                case 5: return "Monte Carmelo";
                case 6: return "Pontal";
                case 7: return "Patos de Minas";
                default: return "Santa Mônica";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class ListPrevisaoResponse
    {
        public List<PrevisaoResponse> PrevisaoAtual { get; set; } = new();
        public List<PrevisaoResponse> PrevisaoAmanha { get; set; } = new();
    }
}
