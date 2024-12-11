using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;

namespace MaxTools
{
    public class NeuralNetwork : IComparable<NeuralNetwork>
    {
        public enum ActivationType
        {
            Tanh,
            Sigmoid,
            ReLU,
            LeakyReLU
        }

        public int[] layers;
        public float[][] neurons;
        public float[][] biases;
        public float[][][] weights;
        public ActivationType activationType;
        public Func<float, float> activate;
        public float efficiency;

        public NeuralNetwork(int[] layers, ActivationType activationType)
        {
            this.layers = new int[layers.Length];

            for (int i = 0; i < layers.Length; ++i)
            {
                this.layers[i] = layers[i];
            }

            neurons = new float[layers.Length][];

            for (int i = 0; i < neurons.Length; ++i)
            {
                neurons[i] = new float[layers[i]];
            }

            biases = new float[layers.Length - 1][];

            for (int i = 0; i < biases.Length; ++i)
            {
                biases[i] = new float[layers[i + 1]];

                for (int j = 0; j < biases[i].Length; ++j)
                {
                    biases[i][j] = Randomize.Diapason(0.5f);
                }
            }

            weights = new float[layers.Length - 1][][];

            for (int i = 0; i < weights.Length; ++i)
            {
                weights[i] = new float[layers[i + 1]][];

                for (int j = 0; j < weights[i].Length; ++j)
                {
                    weights[i][j] = new float[layers[i]];

                    for (int k = 0; k < weights[i][j].Length; ++k)
                    {
                        weights[i][j][k] = Randomize.Diapason(0.5f);
                    }
                }
            }

            this.activationType = activationType;

            switch (activationType)
            {
                case ActivationType.Tanh:
                    activate = Tools.Tanh;
                    break;

                case ActivationType.Sigmoid:
                    activate = Tools.Sigmoid;
                    break;

                case ActivationType.ReLU:
                    activate = Tools.ReLU;
                    break;

                case ActivationType.LeakyReLU:
                    activate = Tools.LeakyReLU;
                    break;
            }
        }
        public NeuralNetwork(NeuralNetwork other)
        {
            other.CopyTo(this);
        }
        public NeuralNetwork(string path)
        {
            Load(path);
        }

        public float[] FeedForward(float[] inputs)
        {
            for (int i = 0; i < inputs.Length; ++i)
            {
                neurons[0][i] = inputs[i];
            }

            for (int i = 1; i < neurons.Length; ++i)
            {
                for (int j = 0; j < neurons[i].Length; ++j)
                {
                    float value = 0.0f;

                    for (int k = 0; k < neurons[i - 1].Length; ++k)
                    {
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }

                    neurons[i][j] = activate(value + biases[i - 1][j]);
                }
            }

            return neurons[neurons.Length - 1];
        }

        public void MutateBiases(float chance, float intensity)
        {
            for (int i = 0; i < biases.Length; ++i)
            {
                for (int j = 0; j < biases[i].Length; ++j)
                {
                    if (Randomize.Chance(chance))
                    {
                        biases[i][j] += Randomize.Diapason(intensity);
                    }
                }
            }
        }
        public void MutateWeights(float chance, float intensity)
        {
            for (int i = 0; i < weights.Length; ++i)
            {
                for (int j = 0; j < weights[i].Length; ++j)
                {
                    for (int k = 0; k < weights[i][j].Length; ++k)
                    {
                        if (Randomize.Chance(chance))
                        {
                            weights[i][j][k] += Randomize.Diapason(intensity);
                        }
                    }
                }
            }
        }

        public void RecombineBiases(float chance, NeuralNetwork other)
        {
            for (int i = 0; i < biases.Length; ++i)
            {
                for (int j = 0; j < biases[i].Length; ++j)
                {
                    if (Randomize.Chance(chance))
                    {
                        var bias = biases[i][j];
                        biases[i][j] = other.biases[i][j];
                        other.biases[i][j] = bias;
                    }
                }
            }
        }
        public void RecombineWeights(float chance, NeuralNetwork other)
        {
            for (int i = 0; i < weights.Length; ++i)
            {
                for (int j = 0; j < weights[i].Length; ++j)
                {
                    for (int k = 0; k < weights[i][j].Length; ++k)
                    {
                        if (Randomize.Chance(chance))
                        {
                            var weight = weights[i][j][k];
                            weights[i][j][k] = other.weights[i][j][k];
                            other.weights[i][j][k] = weight;
                        }
                    }
                }
            }
        }

        public void CopyTo(NeuralNetwork other)
        {
            other.layers = new int[layers.Length];

            for (int i = 0; i < layers.Length; ++i)
            {
                other.layers[i] = layers[i];
            }

            other.neurons = new float[neurons.Length][];

            for (int i = 0; i < neurons.Length; ++i)
            {
                other.neurons[i] = new float[neurons[i].Length];

                for (int j = 0; j < neurons[i].Length; ++j)
                {
                    other.neurons[i][j] = neurons[i][j];
                }
            }

            other.biases = new float[biases.Length][];

            for (int i = 0; i < biases.Length; ++i)
            {
                other.biases[i] = new float[biases[i].Length];

                for (int j = 0; j < biases[i].Length; ++j)
                {
                    other.biases[i][j] = biases[i][j];
                }
            }

            other.weights = new float[weights.Length][][];

            for (int i = 0; i < weights.Length; ++i)
            {
                other.weights[i] = new float[weights[i].Length][];

                for (int j = 0; j < weights[i].Length; ++j)
                {
                    other.weights[i][j] = new float[weights[i][j].Length];

                    for (int k = 0; k < weights[i][j].Length; ++k)
                    {
                        other.weights[i][j][k] = weights[i][j][k];
                    }
                }
            }

            other.activationType = activationType;
            other.activate = activate;
            other.efficiency = efficiency;
        }
        public int CompareTo(NeuralNetwork other)
        {
            if (other == null)
                return +1;
            else if (efficiency > other.efficiency)
                return +1;
            else if (efficiency < other.efficiency)
                return -1;
            else
                return +0;
        }

        public void Save(string path)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            List<string> data = new List<string>();

            data.Add(string.Join(" ", layers));

            for (int i = 0; i < neurons.Length; ++i)
            {
                data.Add(string.Join(" ", neurons[i]));
            }

            for (int i = 0; i < biases.Length; ++i)
            {
                data.Add(string.Join(" ", biases[i]));
            }

            for (int i = 0; i < weights.Length; ++i)
            {
                for (int j = 0; j < weights[i].Length; ++j)
                {
                    data.Add(string.Join(" ", weights[i][j]));
                }
            }

            data.Add(activationType.ToString());
            data.Add(efficiency.ToString());

            File.WriteAllLines(path, data, Encoding.UTF8);

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }
        public void Load(string path)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            string[] data = File.ReadAllLines(path, Encoding.UTF8);

            int index = 0;

            layers = data[index++].Split(' ').Select((v) => int.Parse(v, CultureInfo.InvariantCulture)).ToArray();

            neurons = new float[layers.Length][];

            for (int i = 0; i < neurons.Length; ++i)
            {
                neurons[i] = data[index++].Split(' ').Select((v) => float.Parse(v, CultureInfo.InvariantCulture)).ToArray();
            }

            biases = new float[layers.Length - 1][];

            for (int i = 0; i < biases.Length; ++i)
            {
                biases[i] = data[index++].Split(' ').Select((v) => float.Parse(v, CultureInfo.InvariantCulture)).ToArray();
            }

            weights = new float[layers.Length - 1][][];

            for (int i = 0; i < weights.Length; ++i)
            {
                weights[i] = new float[layers[i + 1]][];

                for (int j = 0; j < weights[i].Length; ++j)
                {
                    weights[i][j] = data[index++].Split(' ').Select((v) => float.Parse(v, CultureInfo.InvariantCulture)).ToArray();
                }
            }

            activationType = Tools.GetEnumByName<ActivationType>(data[index++]);

            switch (activationType)
            {
                case ActivationType.Tanh:
                    activate = Tools.Tanh;
                    break;

                case ActivationType.Sigmoid:
                    activate = Tools.Sigmoid;
                    break;

                case ActivationType.ReLU:
                    activate = Tools.ReLU;
                    break;

                case ActivationType.LeakyReLU:
                    activate = Tools.LeakyReLU;
                    break;
            }

            efficiency = float.Parse(data[index], CultureInfo.InvariantCulture);

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }
    }
}
