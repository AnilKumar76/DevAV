﻿namespace DevExpress.OutlookInspiredApp.Win.Modules {
    using DevExpress.OutlookInspiredApp.Win.Presenters;
    using DevExpress.OutlookInspiredApp.Win.ViewModel;
    using DevExpress.XtraLayout.Utils;

    public partial class CustomersFilterPaneCollapsed : BaseModuleControl, ISupportCompactLayout {
        CustomerFilterPanePresenter presenterCore;
        public CustomersFilterPaneCollapsed(CustomerCollectionViewModel collectionViewModel)
            : base(() => CreateViewModel(() => new CustomersFilterTreeViewModel(collectionViewModel))) {
            InitializeComponent();
            presenterCore = CreatePresenter();
            BindCommands();
        }
        protected override void OnDisposing() {
            Presenter.Dispose();
            base.OnDisposing();
        }
        public CustomersFilterTreeViewModel ViewModel {
            get { return GetViewModel<CustomersFilterTreeViewModel>(); }
        }
        public CustomerFilterPanePresenter Presenter {
            get { return presenterCore; }
        }
        protected virtual CustomerFilterPanePresenter CreatePresenter() {
            return new CustomerFilterPanePresenter(navigationBar, ViewModel);
        }
        protected virtual void BindCommands() {
            btnNew.BindCommand(() => Presenter.CollectionViewModel.New(), Presenter.CollectionViewModel);
        }
        #region ISupportCompactLayout Members
        bool compactLayout = true;
        bool ISupportCompactLayout.Compact {
            get { return compactLayout; }
            set {
                if(compactLayout == value) return;
                compactLayout = value;
                UpdateCompactLayout();
            }
        }
        void UpdateCompactLayout() {
            btnNewLayoutControlItem.Visibility = compactLayout ? LayoutVisibility.Never : LayoutVisibility.Always;
        }
        #endregion
    }
}
