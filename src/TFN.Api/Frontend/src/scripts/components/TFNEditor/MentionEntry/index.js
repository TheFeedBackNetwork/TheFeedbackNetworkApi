
import React from 'react';

const MentionEntry = (props) => {
  const {
    mention,
    theme,
    searchValue, // eslint-disable-line no-unused-vars
    ...parentProps
  } = props;
  console.log('theme')
  console.log(props.theme)
  return (
    <div {...parentProps}>
      <div className={theme.mentionSuggestionsEntryContainer}>
        <div className={theme.mentionSuggestionsEntryContainerLeft}>
          <img
            src={mention.get('avatar')}
            className={theme.mentionSuggestionsEntryAvatar}
            role="presentation"
          />
        </div>

        <div className={theme.mentionSuggestionsEntryContainerRight}>
          <div className={theme.mentionSuggestionsEntryText}>
            {mention.get('name')}
          </div>

          <div className={theme.mentionSuggestionsEntryTitle}>
            {mention.get('title')}
          </div>
        </div>
      </div>
    </div>
  );
};

export default MentionEntry;