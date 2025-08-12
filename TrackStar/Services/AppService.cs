using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackStar.Services
{
    public class AppService
    {
        private bool _isLoading = false;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnLoadStateChaged?.Invoke(default!);
            }
        }

        public event Action<object> OnLoadStateChaged;

        
    }
}
