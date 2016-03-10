using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompanySearch.Behaviors
{
	public class EntryCompletedBehavior : Behavior<Entry>
	{
		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EntryCompletedBehavior), null);

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public Entry AssociatedObject { get; private set; }

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			BindingContext = AssociatedObject.BindingContext;
		}

		protected override void OnAttachedTo(Entry bindable)
		{
			base.OnAttachedTo(bindable);

			AssociatedObject = bindable;

			if (bindable.BindingContext != null)
				BindingContext = bindable.BindingContext;

			bindable.BindingContextChanged += onBindingContextChanged;
			bindable.Completed += onCompleted;
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			base.OnDetachingFrom (bindable);

			bindable.BindingContextChanged -= onBindingContextChanged;
			bindable.Completed -= onCompleted;

			AssociatedObject = null;
		}

		private void onBindingContextChanged(object sender, EventArgs e) => OnBindingContextChanged();

		private void onCompleted(object sender, EventArgs e)
		{
			if (Command == null)
				return;

			if (Command.CanExecute(null))
				Command.Execute(null);
		}
	}
}