/* Copyright 2009 dotless project, http://www.dotlesscss.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *     
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License. */

using System;

namespace LessProject.DotLessIntegration
{
    static class GuidList
    {
        public const string guidDotLessIntegrationPkgString = "8e383b66-5101-46b1-bce9-765e6f6d76b3";
        public const string guidDotLessIntegrationCmdSetString = "68579ca5-0d1a-4b39-9f21-6e6fba4305a0";
        public const string guidDotLessIntegrationEditorFactoryString = "604e9e5a-19fc-4e67-b238-f8d3229f4dba";

        public static readonly Guid guidDotLessIntegrationCmdSet = new Guid(guidDotLessIntegrationCmdSetString);
        public static readonly Guid guidDotLessIntegrationEditorFactory = new Guid(guidDotLessIntegrationEditorFactoryString);
    };
}