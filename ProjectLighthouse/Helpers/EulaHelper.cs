namespace LBPUnion.ProjectLighthouse.Helpers
{
    public static class EulaHelper
    {
        public const string License = @"
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.";

        public const string PrivateInstanceNotice = @"This server is a private testing instance. 
Please do not make anything public for now, and keep in mind security isn't as tight as a full release would.";

        // ReSharper disable once UnreachableCode
        public const string PrivateInstanceNoticeOrBlank = ShowPrivateInstanceNotice ? PrivateInstanceNotice : "";

        public const bool ShowPrivateInstanceNotice = false;
    }
}