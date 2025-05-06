using System;
using System.Collections.Generic;

namespace ServicioServidorVPN.utilitarios.Extensions {
    public static class ListExtensions {
        public static IEnumerable<List<T>> Chunk<T>(this List<T> source, int chunkSize) {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(chunkSize <= 0) throw new ArgumentOutOfRangeException(nameof(chunkSize), "Chunk size must be greater than 0.");

            for(int i = 0; i < source.Count; i += chunkSize) {
                yield return source.GetRange(i, Math.Min(chunkSize, source.Count - i));
            }
        }
    }
}
