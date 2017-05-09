using System;

namespace DarkHider
{
    internal class info
    {
        public string RandomInfo()
        {
            var table = new string[7];
            table[0] =
                "Encryption is a term that comes from the science of cryptography. It includes the coding and decoding of messages in order to protect their contents.";
            table[1] = "The very word cryptography has Greek origins. “Kryptos” means hidden and “Graphein” – word";
            table[2] =
                "One of the most ancient forms of encryption is letter substitution.Here’s an example:  Substitute the letters in your message with the next ones. That way “I love pCloud” will become “J mpwf qDmpvE”.";
            table[3] =
                "The oldest encryption attempt known to mankind dates back to the kingdom of Egypt, around two thousand years before Christ. The ciphers are found on the tomb of Khnumhotep II. They may have been, however, a joke or an attempt to create a mystic atmosphere.";
            table[4] =
                "Julius Caesar used encryption in the days of the Roman Empire to cipher letters and messages. Generally, encryption played an important role in many wars and in military circles throughout the years.";
            table[5] =
                "Currently, encryption is the most practical and easy way to protect electronically stored, processed or transmitted data.";
            table[6] =
                "There are 2 parts to any encryption system: the algorithm for doing the transformation and a secret piece of information that specifies the particular transformation (called the key).";


            var rnd = new Random();
            var i = rnd.Next(0, 7);
            return table[i];
        }
    }
}