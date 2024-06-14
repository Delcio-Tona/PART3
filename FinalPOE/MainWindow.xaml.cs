using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;

namespace PART3
{
    public partial class MainWindow : Window
    {
        List<string> list = new List<string>();
        List<string> list_name = new List<string>();
        List<string> list_food_group = new List<string>();
        List<int> list_calories = new List<int>();
        List<int> list_num_step = new List<int>();
        List<string> list_des_step = new List<string>();
        private ObservableCollection<Recipe> Recipes { get; set; } = new ObservableCollection<Recipe>();


        public MainWindow()
        {
            InitializeComponent();
           
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Recipes.Add(new Recipe { Name = "Cheese Sandwich", Ingredients = { new Ingredient { Name = "Cheese", Calories = 100, FoodGroup = "Dairy" }, new Ingredient { Name = "Bread", Calories = 75, FoodGroup = "Grains" } } });
            Recipes.Add(new Recipe { Name = "Salad", Ingredients = { new Ingredient { Name = "Lettuce", Calories = 10, FoodGroup = "Vegetables" }, new Ingredient { Name = "Tomato", Calories = 50, FoodGroup = "Vegetables" } } });
            Recipes.Add(new Recipe { Name = "Fruit Salad", Ingredients = { new Ingredient { Name = "Apple", Calories = 50, FoodGroup = "Fruits" }, new Ingredient { Name = "Banana", Calories = 70, FoodGroup = "Fruits" } } });
            Recipes.Add(new Recipe { Name = "Cheese Sandwich", Ingredients = { new Ingredient { Name = "Cheese", Calories = 100, FoodGroup = "Dairy" }, new Ingredient { Name = "Bread", Calories = 200, FoodGroup = "Grains" } } });

            Lvevent.Items.Add(get_text.Text);
            Lvevent.Items.SortDescriptions.Add(new SortDescription());
            Lv_for_ingredients.Items.Add(get_ingredient_name.Text);
            string b = ((ComboBoxItem)foodtype.SelectedItem).Content.ToString();
            get_food_groop.Items.Add(b);
            LV_calories.Items.Add(get_calory.Text);
            int int_calory = int.Parse(get_calory.Text);
            LV_nymber.Items.Add(get_number_of_step.Text);
            LV_description.Items.Add(get_descrition.Text);

            list.Add(get_ingredient_name.Text.ToString());
            list_name.Add(get_text.Text.ToString());
            list_food_group.Add(b);
            list_calories.Add(int_calory);
            list_num_step.Add(int.Parse(get_number_of_step.Text));
            list_des_step.Add(get_descrition.Text);
            eventview.ItemsSource = Recipes;


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Lvevent.Items.Clear();
            // object iten = Lvevent.SelectedItem;
            //var result = MessageBox.Show($"Do you want to delete:{(string)iten}?", "sur?", MessageBoxButton.YesNo);
            //if(result==MessageBoxResult.Yes)
            // Lvevent.Items.Remove(iten);
            var result = MessageBox.Show($"Do you want to delete?", "sur?", MessageBoxButton.YesNo);
            if(result==MessageBoxResult.Yes)
                Lvevent.Items.Clear();
                Lv_for_ingredients.Items.Clear();
                get_food_groop.Items.Clear();
                LV_calories.Items.Clear();
                LV_nymber.Items.Clear();
                LV_description.Items.Clear();
                Recipes.Clear();
                eventview.ItemsSource= Recipes;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            string Get_text = get_text.Text;
            string ingredient_name = get_ingredient_name.Text;
            int num = int.Parse(get_calory.Text);
            string food = ((ComboBoxItem)foodtype.SelectedItem).Content.ToString();
           

            

            if (list.Contains(ingredient_name)&& list_calories.Contains(num) && list_food_group.Contains(food))
            {
                //MessageBox.Show("well done!");
                foreach (string s in list_name)
                {
                    MessageBox.Show("Display List Recipes: "+s);
                    

                }

            }
            else
            {
                MessageBox.Show("Please select the right ingredient, food group and calories");
            }
           

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           
            //Lvevent.ItemsSource = Recipes;
            var selectedRecipes = eventview.SelectedItems.Cast<Recipe>().ToList();
            if (selectedRecipes.Count == 0)
            {
                MessageBox.Show("Please select at least one recipe");
                return;
            }

            var foodGroupCounts = selectedRecipes
                .SelectMany(r => r.Ingredients)
                .GroupBy(i => i.FoodGroup)
                .Select(g => new { FoodGroup = g.Key, Count = g.Count() })
                .ToList();

            FoodGroupPieChart.Series.Clear();
            foreach (var group in foodGroupCounts)
            {
                FoodGroupPieChart.Series.Add(new PieSeries
                {
                    Title = group.FoodGroup,
                    Values = new ChartValues<int> { group.Count },
                    DataLabels = true
                });
            }
        

        }

        private void get_descrition_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}