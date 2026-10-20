using System;
using System.Collections.Generic;
using System.Linq;

namespace DebugExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var assignments = new List<Assignment>
            {
                new Assignment("A1", 18, 20),
                new Assignment("A2", 17, 20),
                new Assignment("A3", 14, 20), // laveste
                new Assignment("A4", 19, 20),
            };

            var exam = new Exam(72, 100);

            double assignmentPct = AssignmentAverage(assignments, dropLowest: true);
            double examPct = Percent(exam.Points, exam.MaxPoints);

            double score = WeightedTotal(assignmentPct, examPct);

            // En lille kurve (typisk +5)
            score = ApplyCurve(score, curvePoints: 5);

            Console.WriteLine($"Assignment average: {assignmentPct:F2}%");
            Console.WriteLine($"Exam: {examPct:F2}%");
            Console.WriteLine($"Final score: {score:F2}%");
        }

        static double Percent(double points, double maxPoints)
        {
            return (points / maxPoints) * 100;
        }

        static double AssignmentAverage(List<Assignment> assignments, bool dropLowest)
        {
            var scores = assignments
                .Select(a => Percent(a.Points, a.MaxPoints))
                .ToList();

            if (dropLowest && scores.Count > 0)
            {
                double lowest = scores.Min();
                scores.Remove(lowest);
            }

            return scores.Average();
        }

        static double WeightedTotal(double assignmentPct, double examPct)
        {
            // 40% afleveringer, 60% eksamen
            return assignmentPct * 0.40 + examPct * 0.60;
        }

        static double ApplyCurve(double score, double curvePoints)
        {
            score += curvePoints;
            return Math.Min(score, 100.0);
        }
    }

    class Assignment
    {
        public string Name { get; }
        public double Points { get; }
        public double MaxPoints { get; }

        public Assignment(string name, double points, double maxPoints)
        {
            Name = name;
            Points = points;
            MaxPoints = maxPoints;
        }
    }

    class Exam
    {
        public double Points { get; }
        public double MaxPoints { get; }

        public Exam(double points, double maxPoints)
        {
            Points = points;
            MaxPoints = maxPoints;
        }
    }
}
