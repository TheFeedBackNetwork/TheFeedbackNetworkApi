
import React from 'react';

const MentionEntry = (props) => {
  const {
    mention,
    theme,
    searchValue, // eslint-disable-line no-unused-vars
    ...parentProps
  } = props;
  return (
    <div {...parentProps}>
      <div className='mentionSuggestionsEntryContainer'>
        <div className='theme.mentionSuggestionsEntryContainerLeft'>
          <img
            src={mention.get('avatar')}
            className='mentionSuggestionsEntryAvatar'
            role="presentation"
          />
        </div>

        <div className='mentionSuggestionsEntryContainerRight'>
          <div className='mentionSuggestionsEntryText'>
            {mention.get('name')}
          </div>

          <div className='mentionSuggestionsEntryTitl'>
            {mention.get('title')}
          </div>
        </div>
      </div>
    </div>
  );
};

export default MentionEntry;