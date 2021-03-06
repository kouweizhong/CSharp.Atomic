﻿////////////////////////////////////////////////////////////////////////////////
//
//  MATTBOLT.BLOGSPOT.COM
//  Copyright(C) 2013 Matt Bolt
//
//  Permission is hereby granted, free of charge, to any person obtaining a 
//  copy of this software and associated documentation files (the "Software"), 
//  to deal in the Software without restriction, including without limitation 
//  the rights to use, copy, modify, merge, publish, distribute, sublicense, 
//  and/or sell copies of the Software, and to permit persons to whom the 
//  Software is furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included 
//  in all copies or substantial portions of the Software
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
//  THE SOFTWARE.
//
////////////////////////////////////////////////////////////////////////////////

namespace CSharp.Collections.Concurrent {

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using CSharp.Threading;
    using CSharp.Locking;

    /// <summary>
    /// This class serves as a thread-safe wrapper for an <c>IEnumerator</c> implementation.
    /// It locks as soon as the instance is created and will release the lock once disposed.
    /// </summary>
    /// <typeparam name="T">The type of the enumerator.</typeparam>
    public class LockingEnumerator<T> : IEnumerator<T>, IEnumerator, IDisposable {
        private readonly IEnumerator<T> _enumerator;
        private readonly ILock _lock;

        public LockingEnumerator(IEnumerator<T> enumerator, ILock @lock) {
            _enumerator = enumerator;
            _lock = @lock;

            _lock.Lock();
        }

        public void Dispose() {
            _lock.Unlock();
        }

        public bool MoveNext() {
            return _enumerator.MoveNext();
        }

        public void Reset() {
           _enumerator.Reset();
        }

        public T Current {
            get {
                return _enumerator.Current;
            }
        }

        object IEnumerator.Current {
            get {
                return Current;
            }
        }
    }
}
