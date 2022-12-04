namespace Lab2
{
    public class Field
    {
        private List<Snail> Snails { get; set; }

        public Field()
        {
            Snails = new List<Snail>();
        }

        public void ClearField()
        {
            Snails.Clear();
        }

        public void AddSnail(double x, double y)
        {
            Snails.Add(new Snail()
            {
                X = x,
                Y = y
            });
        }

        public double Solve()
        {
            Snails = Snails.OrderBy(snail => snail.X).ToList(); //sort by x
            return Solver(0, Snails.Count - 1);
        }

        private double Solver(int left_border, int right_border)
        {
            if(right_border - left_border == 1)
            {
                //sort by y as well
                if (Snails[left_border].Y > Snails[right_border].Y)
                {
                    var t = Snails[left_border];
                    Snails[left_border] = Snails[right_border];
                    Snails[right_border] = t;
                }
                return Math.Sqrt(Math.Pow(Snails[left_border].X - Snails[right_border].X, 2) + Math.Pow(Snails[left_border].Y - Snails[right_border].Y, 2));
            }

            if(right_border == left_border)
            {
                return double.MaxValue;
            }

            //split field into two parts
            int middle = left_border + (right_border - left_border)/2;
            //recursive part
            double temp = Math.Min(
                Solver(left_border, middle),
                Solver(middle + 1, right_border));


            //merging two parts to sort by y
            int firstIndex = left_border;
            int secondIndex = middle + 1;
            var mergedPart = new List<Snail>();
            for(int i = 0; i < right_border - left_border + 1; i++)
            {
                if(Snails[firstIndex].Y > Snails[secondIndex].Y)
                {
                    mergedPart.Add(Snails[secondIndex]);
                    secondIndex++;
                    if (secondIndex == right_border + 1)
                    {
                        i++;
                        while(i < right_border - left_border + 1)
                        {
                            mergedPart.Add(Snails[firstIndex]);
                            firstIndex++;
                            i++;
                        }
                        break;
                    }
                }
                else
                {
                    mergedPart.Add(Snails[firstIndex]);
                    firstIndex++;
                    if (firstIndex == middle + 1)
                    {
                        i++;
                        while (i < right_border - left_border + 1)
                        {
                            mergedPart.Add(Snails[secondIndex]);
                            secondIndex++;
                            i++;
                        }
                        break;
                    }
                }
            }
            for (int i = 0; i < right_border - left_border + 1; i++)
            {
                Snails[left_border + i] = mergedPart[i];
            }

            //checking if there is closer snails in different halfs
            double min = double.MaxValue;
            double middleX = (Snails[middle].X + Snails[middle + 1].X) / 2;
            for (int i = left_border; i < right_border; i++)
            {
                if (Math.Abs(Snails[i].X - middleX) < temp)
                {
                    for(int j = i + 1; j < right_border + 1; j++)
                    {
                        if (Math.Abs(Snails[i].Y - Snails[j].Y) < temp)
                        {
                            double distance = Math.Sqrt(Math.Pow(Snails[i].X - Snails[j].X, 2) + Math.Pow(Snails[i].Y - Snails[j].Y, 2));
                            if (distance < min)
                            {
                                min = distance;
                            }
                        }
                        j++;
                    }
                }
            }
            return Math.Min(min, temp);
        }
    }
}
