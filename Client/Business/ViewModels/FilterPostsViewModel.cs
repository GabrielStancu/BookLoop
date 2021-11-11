using Business.Helpers;
using Business.Shared;
using Data.Specification;
using System.Collections.ObjectModel;

namespace Business.ViewModels
{
    public class FilterPostsViewModel: BaseViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Location { get; set; }
        public double? MinimumPrice { get; set; }
        public double? MaximumPrice { get; set; }

        private OfferType _offerType = OfferType.AnyOffer;
        public OfferType OfferType 
        { 
            get { return _offerType; }
            set
            {
                _offerType = value;
                SetProperty<OfferType>(ref _offerType, value);
            }
        }
        private PriceSorting _priceSorting = PriceSorting.NoOrder;
        public PriceSorting PriceSorting 
        {
            get { return _priceSorting; }
            set
            {
                _priceSorting = value;
                SetProperty<PriceSorting>(ref _priceSorting, value);

                if(value != PriceSorting.NoOrder)
                {
                    TitleSorting = TitleSorting.NoOrder;
                }
            }
        }
        private TitleSorting _titleSorting = TitleSorting.NoOrder;
        public TitleSorting TitleSorting 
        {
            get { return _titleSorting; }
            set
            {
                _titleSorting = value;
                SetProperty<TitleSorting>(ref _titleSorting, value);

                if(value != TitleSorting.NoOrder)
                {
                    PriceSorting = PriceSorting.NoOrder;
                }
            }
        }
        public ObservableCollection<OfferType> OfferTypes { get; set; }
        public ObservableCollection<PriceSorting> PriceSortings { get; set; }
        public ObservableCollection<TitleSorting> TitleSortings { get; set; }

        public FilterPostsViewModel()
        {
            InitPickersDataSources();
            LoadFilterOptions();
        }

        private void LoadFilterOptions()
        {
            var specification = DataContainer.Specification;

            Title = string.IsNullOrEmpty(specification.Title) ? null : specification.Title;
            Author = string.IsNullOrEmpty(specification.Author) ? null : specification.Author;
            Genre = string.IsNullOrEmpty(specification.Genre) ? null : specification.Genre;
            Location = string.IsNullOrEmpty(specification.Location) ? null : specification.Location;
            MinimumPrice = specification.MinimumPrice == Specification.DefaultMinimumPrice ? null : specification.MinimumPrice;
            MaximumPrice = specification.MaximumPrice == Specification.DefaultMaximumPrice ? null : specification.MaximumPrice;
            OfferType = specification.OfferType;
            PriceSorting = specification.PriceSorting;
            TitleSorting = specification.TitleSorting;
    }

        public void CreateSpecification()
        {
            var specification = new Specification()
            {
                Title = Title ?? string.Empty,
                Author = Author ?? string.Empty,
                Genre = Genre ?? string.Empty,
                Location = Location ?? string.Empty,
                MinimumPrice = MinimumPrice, 
                MaximumPrice = MaximumPrice,
                OfferType = OfferType,
                PriceSorting = PriceSorting, 
                TitleSorting = TitleSorting,
                PageNumber = 1,
                UserId = DataContainer.GetUserId()
            };

            DataContainer.Specification = specification;
        }

        private void InitPickersDataSources()
        {
            OfferTypes = new OptionsLoader<OfferType>().LoadOptions();
            PriceSortings = new OptionsLoader<PriceSorting>().LoadOptions();
            TitleSortings = new OptionsLoader<TitleSorting>().LoadOptions();
        }
    }
}
