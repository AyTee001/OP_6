using System.Threading.Channels;

namespace ОП_ПР_6
{
    internal class Program
    {
        public static Dictionary<MyEnum, string> shopList = new Dictionary<MyEnum, string>
        {
            {MyEnum.HighHeels, "Туфлі на каблуках"},
            {MyEnum.Trainers, "Кросівки"},
            {MyEnum.Chelsea, "Челсі"},
            {MyEnum.Jacket, "Піджак"},
            {MyEnum.Shirt, "Рубашка"},
            {MyEnum.Jeans, "Джинси"},
            {MyEnum.Mobile, "Телефон"},
            {MyEnum.Laptop, "Ноутбук"},
            {MyEnum.Monitor, "Монітор"},
        };
        public static void AddItem(int prodNum, out MyEnum i, out MyEnum c)
        {
            i = (MyEnum)(int)(Math.Pow(2, prodNum - 1));
            if((MyEnum.Shoes & i) != 0)
            {
                c = MyEnum.Shoes;
            }
            else if((MyEnum.Clothes & i) != 0)
            {
                c = MyEnum.Clothes;
            }
            else
            {
                c = MyEnum.Technology;
            }
        }

        [Flags]
        public enum MyEnum
        {
            None = 0b000000000,

            HighHeels = 0b000000001,
            Trainers = 0b000000010,
            Chelsea = 0b000000100,

            Shoes = 0b000000111,

            Jacket = 0b000001000,
            Shirt = 0b000010000,
            Jeans = 0b000100000,

            Clothes = 0b000111000,

            Mobile = 0b001000000,
            Laptop = 0b010000000,
            Monitor = 0b100000000,

            Technology = 0b111000000

        }

        static void Main()
        {
            Console.InputEncoding = Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Список доступних товарів: ");

            //Список товарів
            int m = 1;
            Console.WriteLine("Взуття");
            foreach(var item in shopList)
            {
                if((item.Key & MyEnum.Shoes) != 0)
                {
                    Console.WriteLine(m + ". " + item.Value);
                }
                m++;
            }            
            Console.WriteLine("Одяг");
            m = 1;
            foreach(var item in shopList)
            {
                if ((item.Key & MyEnum.Clothes) != 0)
                {
                    Console.WriteLine(m + ". " + item.Value);
                }
                m++;
            }
            m = 1;
            Console.WriteLine("Електротовари");
            foreach(var item in shopList)
            {
                if ((item.Key & MyEnum.Technology) != 0)
                {
                    Console.WriteLine(m + ". " + item.Value);
                }
                m++;
            }

            //Запит користувача
            Console.WriteLine("Введіть список товарів, що Ви бажаєте отримати (будь ласка, відокремлюйте числа комами та не використовуйте інших знаків): ");
            var strArr = Console.ReadLine().Split(',', ' ').Where(i => i != "");


            //Обробка
            List<MyEnum> shoes = new ();
            List<MyEnum> clothes = new ();
            List<MyEnum> technology = new ();

            foreach (string i in strArr)
            {
                if (int.TryParse(i, out int num)){
                    if (1 <= num && num <= 9)
                    {
                        AddItem(num, out MyEnum item, out MyEnum category);
                        switch (category)
                        {
                            case MyEnum.Shoes:
                                shoes.Add(item);
                                break;
                            case MyEnum.Clothes:
                                clothes.Add(item);
                                break;
                            case MyEnum.Technology:
                                technology.Add(item);
                                break;
                        }
                    }
                    else { Console.WriteLine($"*** Немає товару з номером {num}*** \n"); }
                }
                else
                {
                    Console.WriteLine("*** Деякі дані введено неправильно, частина замовлення не буде опрацьована *** \n");
                }
            }

            //Вибір за категоріями
            Console.WriteLine("\nЗараз у Вашому кошику (за категоріями товарів)");
            Console.WriteLine("Взуття: ");
            foreach (var item in shoes) { Console.WriteLine("-" + shopList[item]); }
            Console.WriteLine("\nОдяг:");
            foreach(var item in clothes) { Console.WriteLine("-" + shopList[item]); }
            Console.WriteLine("\nЕлектротовари:");
            foreach (var item in technology) { Console.WriteLine("-" + shopList[item]); }

            Console.WriteLine("\n\n*************************************************");
            Console.WriteLine("Програма розроблена Тютюн А.В., ФІТ 1-5 ");
        }
    }
}