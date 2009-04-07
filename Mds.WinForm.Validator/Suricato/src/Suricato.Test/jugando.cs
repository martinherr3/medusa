using System;
using Castle.Components.Validator;
using Suricato.Validation;
using uNhAddIns.ActiveRecord;

namespace Suricato.Test
{
    public class jugando
    {
        public void metodo() {
            
            Product product = new Product();
            product.Name = "n1";
            product.Price = 34343.343;

            Business<Product>.Validate(product)
                .Then(delegate 
                { 
                    product.MakeSomething(); 
                })
                .OnError(delegate 
                {
                    foreach (string error in Business<Product>.Errors)
                    {
                        Console.WriteLine("error: " + error); 
                    }
                });

            Business<Product>.Validate(product)
                .Then(delegate
                {
                    product.MakeSomething();
                })
                .OnEachError(delegate(string error)
                {
                   Console.WriteLine("error: " + error);
                });
        }
    }

    internal class Product : ActiveRecordValidationBase<Product> , IValidatable
    {
        private string name;
        private double price;

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public double Price {
            get { return price; }
            set { price = value; }
        }

        public void MakeSomething() {
        }
    }
}