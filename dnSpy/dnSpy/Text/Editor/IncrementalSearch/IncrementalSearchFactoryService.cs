/*
    Copyright (C) 2014-2019 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.IncrementalSearch;
using Microsoft.VisualStudio.Text.Operations;

namespace dnSpy.Text.Editor.IncrementalSearch {
	[Export(typeof(IIncrementalSearchFactoryService))]
	sealed class IncrementalSearchFactoryService : IIncrementalSearchFactoryService {
		readonly ITextSearchService textSearchService;
		readonly IEditorOperationsFactoryService editorOperationsFactoryService;

		[ImportingConstructor]
		IncrementalSearchFactoryService(ITextSearchService textSearchService, IEditorOperationsFactoryService editorOperationsFactoryService) {
			this.textSearchService = textSearchService;
			this.editorOperationsFactoryService = editorOperationsFactoryService;
		}

		public IIncrementalSearch GetIncrementalSearch(ITextView textView) {
			if (textView == null)
				throw new ArgumentNullException(nameof(textView));
			return textView.Properties.GetOrCreateSingletonProperty(typeof(IIncrementalSearch), () => new IncrementalSearch(textView, textSearchService, editorOperationsFactoryService));
		}
	}
}
