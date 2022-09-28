using System;
using System.Runtime.InteropServices;
using CERTENROLLLib;
using WinCryptKeyExplorer.ViewModels;

namespace WinCryptKeyExplorer.Models {
    class CspProvAlg : ViewModelBase {
        public CspProvAlg(ICspAlgorithm alg) {
            Name = alg.Name;
            LongName = alg.LongName;
            AlgorithmType = (AlgorithmType2)alg.Type;
            AlgorithmOperations = (AlgorithmOperationFlags2)alg.Operations;
            DefaultLength = alg.DefaultLength;
            MinLength = alg.MinLength;
            MaxLength = alg.MaxLength;
            IncrementLength = alg.IncrementLength;
            IsValid = alg.Valid;
            Marshal.ReleaseComObject(alg);
        }

        /// <summary>
        /// Gets the abbreviated algorithm name.
        /// </summary>
        public String Name { get; }
        /// <summary>
        /// Gets the full name of the algorithm.
        /// </summary>
        public String LongName { get; }
        /// <summary>
        /// Gets the algorithm type.
        /// </summary>
        public AlgorithmType2 AlgorithmType { get; }
        /// <summary>
        /// Gets the operations that can be performed by the algorithm.
        /// </summary>
        public AlgorithmOperationFlags2 AlgorithmOperations { get; }
        /// <summary>
        /// Gets the default length of a key.
        /// </summary>
        public Int32 DefaultLength { get; }
        /// <summary>
        /// Gets the minimum permitted length for a key.
        /// </summary>
        public Int32 MinLength { get; }
        /// <summary>
        /// Gets the maximum permitted length for a key.
        /// </summary>
        public Int32 MaxLength { get; }
        /// <summary>
        /// Gets a value, in bits, that can be used to determine valid incremental key lengths for algorithms that
        /// support multiple key sizes.
        /// </summary>
        public Int32 IncrementLength { get; }
        /// <summary>
        /// Gets a Boolean value that specifies whether the algorithm object is valid.
        /// </summary>
        public Boolean IsValid { get; }
    }
}
