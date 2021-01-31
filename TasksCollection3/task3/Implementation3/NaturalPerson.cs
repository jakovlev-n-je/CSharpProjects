using System.Collections.Generic;
using System.Text;

namespace Implementation3
{
    public class NaturalPerson
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int ChildCount { get; set; }

        public List<Revenue> Revenues { get; set; }

        public List<Tax> Taxes { get; set; }

        public NaturalPerson(string name, string surname, int childCount, List<Revenue> revenues)
        {
            name = name.ToLower();
            Name = char.ToUpper(name[0]) + name.Substring(1);
            surname = surname.ToLower();
            Surname = char.ToUpper(surname[0]) + surname.Substring(1);
            ChildCount = childCount;
            Revenues = revenues;
            InitializeTaxes();
            SortTaxes();
        }

        public bool Equals(NaturalPerson person)
        {
            return Name == person.Name && Surname == person.Surname && ChildCount == person.ChildCount;
        }

        public string GetInformation()
        {
            StringBuilder builder = new StringBuilder($"Имя: {Name}" +
                                                      $"\nФамилия: {Surname}" +
                                                      $"\nДетей: {ChildCount}" +
                                                      "\n\nДоходы:" +
                                                      (Revenues.Count > 0 ? "" : "\nОтстутствуют"));
            for (int i = 0; i < Revenues.Count; i++)
            {
                builder.Append(Taxes[i].Revenue.GetInformation() +
                               Taxes[i].GetInformation());
            }
            return builder.ToString();
        }

        public override string ToString()
        {
            return $"{Surname} " +
                   $"{Name[0]}.";
        }

        private void SortTaxes()
        {
            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = true;
                for (int i = 1; i < Taxes.Count; i++)
                {
                    if (Taxes[i].TaxAmount < Taxes[i - 1].TaxAmount)
                    {
                        Tax tmp = Taxes[i];
                        Taxes[i] = Taxes[i - 1];
                        Taxes[i - 1] = tmp;
                        isSorted = false;
                    }
                }
            }
        }

        private void InitializeTaxes()
        {
            Taxes = new List<Tax>();
            foreach (Revenue revenue in Revenues)
            {
                Taxes.Add(new Tax(revenue, ChildCount));
            }
        }
    }
}
