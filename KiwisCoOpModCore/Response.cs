/*
    Kiwi's Co-Op Mod for Half-Life: Alyx
    Copyright (c) 2022 KiwifruitDev
    All rights reserved.
    This software is licensed under the MIT License.
    -----------------------------------------------------------------------------
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
    -----------------------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KiwisCoOpModCore
{
    public class Response
    {
        public string? type;
        public string? data;
        public string? map;
        public string? remoteClientUsername;
        public string? clientUsername;
        public string? password;
        public readonly static int internalVersion = 0;
        public int? version = internalVersion;
        public bool urgent;
        public Response()
        { }
        public Response(string type)
        {
            this.type = type;
        }
        public Response(string type, string data)
        {
            this.type = type;
            this.data = data;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
