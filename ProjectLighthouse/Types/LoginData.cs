using System.IO;
using System.Text;
using ProjectLighthouse.Helpers;

namespace ProjectLighthouse.Types {
    // This is all the information I can understand for now. More testing is required.
    // Example data:
    //  - LBP2 digital, with the RPCN username `literally1984`
    //      POST /LITTLEBIGPLANETPS3_XML/login?applicationID=21414&languageID=1&lbp2=1&beta=0&titleID=NPUA80662&country=us
    //      !�0256333||x||��Y literally198bruUP9000-NPUA80662_008D
    //  - LBP2 digital, with the RPCN username `jvyden`
    //      POST /LITTLEBIGPLANETPS3_XML/login?applicationID=21414&languageID=1&lbp2=1&beta=0&titleID=NPUA80662&country=us
    //      !�0220333||/u||=0� jvydebruUP9000-NPUA80662_008D
    /// <summary>
    /// The data sent from POST /LOGIN.
    /// </summary>
    public class LoginData {
        public string Username { get; set; }
//        public string GameVersion { get; set; }
//        public int UnknownNumber { get; set; } // Seems to increment by 1000 every login attempt

        public static LoginData CreateFromString(string str) {
            do {
                str = str.Replace("\b", string.Empty); // Trim backspace characters
            } while(str.Contains('\b'));

            using MemoryStream ms = new(Encoding.ASCII.GetBytes(str));
            using BinaryReader reader = new(ms);

            LoginData loginData = new();

            BinaryHelper.ReadUntilByte(reader, 0x20); // Skips to relevant part

//            byte[] endBytes = reader.ReadBytes((int)(ms.Length - reader.BaseStream.Position));
//            string end = Encoding.ASCII.GetString(endBytes);
            
            loginData.Username = BinaryHelper.ReadString(reader).Replace("\0", string.Empty);

            return loginData;
        }
    }
}