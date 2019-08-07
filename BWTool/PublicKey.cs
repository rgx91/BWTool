// Copyright 2012 Mike Caldwell (Casascius)
// This file is part of Bitcoin Address Utility.

// Bitcoin Address Utility is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Bitcoin Address Utility is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Bitcoin Address Utility.  If not, see http://www.gnu.org/licenses/.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace Casascius.Bitcoin
{

    /// <summary>
    /// Bitcoin address extended to include knowledge of public key.
    /// </summary>
    public class PublicKey : AddressBase
    {

        protected PublicKey() { }


        private byte[] _publicKey = null;

        public bool IsCompressedPoint { get; protected set; }


        /// <summary>
        /// Virtual method to compute public key on demand when doing so is expensive.
        /// Not used if we are handed a public key through the constructor, but this is used
        /// if a descendant class (e.g. KeyPair) has a private key and the only way to know
        /// the public key is to compute it.
        /// </summary>
        protected virtual byte[] ComputePublicKey() { return null; }

        /// <summary>
        /// Returns the public key bytes.  This will return 65 bytes for an uncompressed public key
        /// or 33 bytes for a compressed public key.
        /// </summary>
        public byte[] PublicKeyBytes
        {
            get
            {
                if (_publicKey == null) _publicKey = ComputePublicKey();

                byte[] rv = new byte[_publicKey.Length];
                _publicKey.CopyTo(rv, 0);
                return rv;
            }
            protected set
            {
                _publicKey = new byte[value.Length];
                value.CopyTo(_publicKey, 0);
            }
        }

        /// <summary>
        /// Computes the Hash160 of the public key upon demand.
        /// </summary>
        protected override byte[] ComputeHash160()
        {
            byte[] shaofpubkey = Util.ComputeSha256(PublicKeyBytes);
            RIPEMD160 rip = System.Security.Cryptography.RIPEMD160.Create();
            return rip.ComputeHash(shaofpubkey);
        }

        /// <summary>
        /// Hexadecimal representation of public key.  Each byte is 2 hex digits, uppercase,
        /// delimited with spaces.
        /// </summary>
        public string PublicKeyHex
        {

            get
            {
                return Util.ByteArrayToString(PublicKeyBytes);
            }
        }



    }
}