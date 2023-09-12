using InterviewProject.Services;
namespace UnitTests
{
    public class Tests
    {
        private ProductsService repository;
        [SetUp]
        public void Setup()
        {
            repository = new ProductsService();
        }
        [Test]
        public void AddNewProduct_WithValidInput()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
            DateTime createdAt = DateTime.Now;
            repository.AddNewProduct(name, description, plnPrice, createdAt);

            var products = repository.GetByName("Test product", "find");
            Assert.Multiple(() =>
            {
                Assert.That(products.Description, Is.EqualTo(description));
                Assert.That(products.PlnPrice, Is.EqualTo(Convert.ToDouble(plnPrice)));
                Assert.That(products.Created, Is.EqualTo(createdAt));
            });
        }

        [Test]
        public void AddNewProduct_WithInvalidPrice()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "not a number";
            string Message = "The value 'not a number' is not a valid number.";
            DateTime createdAt = DateTime.Now;
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.AddNewProduct(name, description, plnPrice, createdAt);
            
            Assert.That(writer.ToString().Trim(), Is.EqualTo(Message));
        }
        [Test]
        public void AddNewProduct_WithEmptyName()
        {
            string name = "";
            string description = "This is a test product";
            string plnPrice = "22";
            string Message = "Invalid input. Field NAME can not be empty";
            DateTime createdAt = DateTime.Now;
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.AddNewProduct(name, description, plnPrice, createdAt);

            Assert.That(writer.ToString().Trim(), Is.EqualTo(Message));
        }
        [Test]
        public void AddNewProduct_WithEmptyPrice()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "";
            string Message = "Invalid input. Field PRICE can not be empty";
            DateTime createdAt = DateTime.Now;
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.AddNewProduct(name, description, plnPrice, createdAt);

            Assert.That(writer.ToString().Trim(), Is.EqualTo(Message));
        }
        [Test]
        public void AddNewProduct_WithEmptyPriceAndName()
        {
            string name = "";
            string description = "This is a test product";
            string plnPrice = "";
            string Message = "Invalid input. Field NAME can not be empty";
            DateTime createdAt = DateTime.Now;
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.AddNewProduct(name, description, plnPrice, createdAt);

            Assert.That(writer.ToString().Trim(), Is.EqualTo(Message));
        }
        [Test]
        public void CheckValidation_InvalidPrice()
        {
            var name = "Product name";
            var price = "invalid price";
            repository.CheckValidation(name, price);
            
            Assert.That(repository.CheckValidation(name, price), Is.False);
;       }
        [Test]
        public void CheckValidation_PriceLowerThanZero()
        {
            var name = "Product name";
            var price = "-200";
            repository.CheckValidation(name, price);

            Assert.That(repository.CheckValidation(name, price), Is.False);
        }
        [Test]
        public void CheckValidation_InvalidName()
        {
            var name = "";
            var price = "invalid price";
            repository.CheckValidation(name, price);

            Assert.That(repository.CheckValidation(name, price), Is.False);
        }
        [Test]
        public void CheckValidation_NullName()
        {
            string? name = null;
            var price = "invalid price";
            repository.CheckValidation(name, price);

            Assert.That(repository.CheckValidation(name, price), Is.False);
        }
        [Test]
        public void CheckValidation_NullPrice()
        {
            var name = "Product name";
            string? price = null;
            repository.CheckValidation(name, price);

            Assert.That(repository.CheckValidation(name, price), Is.False);   
        }
        [Test]
        public void CheckValidation_ValidData()
        {
            var name = "Product name";
            string price ="200";
            repository.CheckValidation(name, price);

            Assert.That(repository.CheckValidation(name, price), Is.True);
        }
        [Test]
        public void GetByName_WithInvalidInput()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
            DateTime createdAt = DateTime.Now;
            var Message = "Product with name 'Test product not found' not found...";
            
            repository.AddNewProduct(name, description, plnPrice, createdAt);
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.GetByName("Test product not found", "find");

            Assert.That(writer.ToString().Trim(), Is.EqualTo(Message));          
        }
        [Test]
        public void GetByName_WithvalidInput()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
            DateTime createdAt = DateTime.Now;
           
            repository.AddNewProduct(name, description, plnPrice, createdAt);
            var writer = new StringWriter();
            Console.SetOut(writer);
            var products = repository.GetByName(name, "find");
            var Message = $"Product found: {name}\nPLN Price: {plnPrice}z³\nDescription: {description}\nModyfied time: {createdAt}\nID: {products.Id}";

            Assert.That(writer.ToString().Trim(), Is.EqualTo(Message));
        }
        [Test]
        public void CalculateRate_validData()
        {
            double EURRate = 4.74;
            double myPrice = 9000.28;
            double Result=Math.Round(myPrice/EURRate, 2);

            Assert.That(repository.CaluclateRate(myPrice, EURRate), Is.EqualTo(Result));
        }
        [Test]
        public void CalculateRate_invalidPrice()
        {
            double EURRate = 4.74;
            double myPrice = -9000.28;
           
            var myException = Assert.Throws<ArgumentException>(() => repository.CaluclateRate(myPrice, EURRate));

            string message = "Value must be positive.";
            Assert.That(myException.Message, Is.EqualTo(message));
        }
        [Test]
        public void GetTheCheapest_WithValidInput()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
          
            for(int i = 0; i < 10; i++)
                repository.AddNewProduct(name+i, description, plnPrice+i, DateTime.Now);

            var products = repository.GetTheCheapest();
            Assert.That(products.Name, Is.EqualTo(name + "0"));
        }
        [Test]
        public void GetTheCheapest_WithValidInput_TheSamePrice()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
           
            for (int i = 0; i < 10; i++)
                repository.AddNewProduct(name+i, description, plnPrice, DateTime.Now);

            var products = repository.GetTheCheapest();
            Assert.That(products.Name, Is.EqualTo(name + "0"));
        }
        [Test]
        public void GetTheCheapest_WithNoData()
        {
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.GetTheCheapest();
            string message="Products not found...";
            Assert.That(writer.ToString().Trim(), Is.EqualTo(message));
        }
        [Test]
        public void GetTheMostExpensive_WithValidInput()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
           
            for (int i = 0; i < 10; i++)
                repository.AddNewProduct(name + i, description, plnPrice +i, DateTime.Now);

            var products = repository.GetTheMostExpensive();
            Assert.That(products.Name, Is.EqualTo(name + "9"));
        }
        [Test]
        public void GetTheMostExpensive_WithValidInput_TheSamePrice()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
           
            for (int i = 0; i < 10; i++)
                repository.AddNewProduct(name + i, description, plnPrice, DateTime.Now);

            var products = repository.GetTheMostExpensive();
            Assert.That(products.Name, Is.EqualTo(name + "0"));
        }
        [Test]
        public void GetTheMostExpensive_WithNoData()
        {
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.GetTheMostExpensive();
            string message = "Products not found...";
            Assert.That(writer.ToString().Trim(), Is.EqualTo(message));
        }
        [Test]
        public void GetLastEdited_WithValidInput()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
         
            for (int i = 0; i < 10; i++)
                repository.AddNewProduct(name + i, description, plnPrice + i, DateTime.Now);

            var products = repository.GetTheNewest();
            Assert.That(products.Name, Is.EqualTo(name + "9"));
        }
        [Test]
        public void GetLastEdited_WithValidInput_WithTheSameDateTime()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
            DateTime createdAt = DateTime.Now;
           
            for (int i = 0; i < 10; i++)
                repository.AddNewProduct(name + i, description, plnPrice, createdAt);

            var products = repository.GetTheNewest();
            Assert.That(products.Name, Is.EqualTo(name + "0"));
        }
        [Test]
        public void GetLastEdited_WithNoData()
        {

            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.GetTheNewest();
            string message = "Products not found...";
            Assert.That(writer.ToString().Trim(), Is.EqualTo(message));

        }
        [Test]
        public void Update_WithValidInput()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
           
            for (int i = 0; i < 10; i++)
                repository.AddNewProduct(name + i, description, plnPrice, DateTime.Now);
            string newName = "Test product new name";
            string newDescription = "This is new test description";
            string newPrice = "999";
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.Update(newName, newPrice, newDescription, name+0);
            var closeWriter = writer.ToString().Trim();
          
            string message= $"Product Updated to: {newName}\nPLN Price: {newPrice}z³\nDescription: {newDescription}\nModyfied time: {repository.GetByName(newName,"find").Created}\nID: {repository.GetByName(newName,"find").Id}";
            Assert.That(closeWriter, Is.EqualTo(message));

        }
        [Test]
        public void Update_WithInvalidInput()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";


            for (int i = 0; i < 10; i++)
                repository.AddNewProduct(name + i, description, plnPrice, DateTime.Now);
            string newName = "Test product new name";
            string newDescription = "This is new test description";
            string newPrice = "asd";
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.Update(newName, newPrice, newDescription, name + 0);
            var closeWriter = writer.ToString().Trim();

            string message = $"The value '{newPrice}' is not a valid number.";
            Assert.That(closeWriter, Is.EqualTo(message));
        }
        [Test]
        public void Update_WithEmptyInput()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";


            for (int i = 0; i < 10; i++)
                repository.AddNewProduct(name + i, description, plnPrice, DateTime.Now);
            string newName = "";
            string newDescription = "";
            string newPrice = "";
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.Update(newName, newPrice, newDescription, name + 0);
            var closeWriter = writer.ToString().Trim();
            Assert.That(closeWriter, Is.Empty);


        }
        [Test]
        public void Update_WithValidNotFullfilledInput()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";


            for (int i = 0; i < 10; i++)
                repository.AddNewProduct(name + i, description, plnPrice, DateTime.Now);
            string newName = "";
            string newDescription = "This is new test description";
            string newPrice = "999";
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.Update(newName, newPrice, newDescription, name + 0);
            var closeWriter = writer.ToString().Trim();

            string message = $"Product Updated to: {name+0}\nPLN Price: {newPrice}z³\nDescription: {newDescription}\nModyfied time: {repository.GetByName(name+0, "find").Created}\nID: {repository.GetByName(name+0, "find").Id}";
            Assert.That(closeWriter, Is.EqualTo(message));
        }
        [Test]
        public void Update_WithInValidPrice()
        {
            string name = "Test product";
            string description = "This is a test product";
            string plnPrice = "100";
                repository.AddNewProduct(name, description, plnPrice, DateTime.Now);
            string newName = "";
            string newDescription = "This is new test description";
            string newPrice = "-999";
            var writer = new StringWriter();
            Console.SetOut(writer);
            repository.Update(newName, newPrice, newDescription, name);
            var closeWriter = writer.ToString().Trim();

            string message = $"The value '{newPrice}' is not a valid number.";
            Assert.That(closeWriter, Is.EqualTo(message));
        }
    }
}