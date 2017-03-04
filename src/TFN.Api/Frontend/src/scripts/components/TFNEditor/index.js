import React, { Component, PropTypes } from 'react';
import Editor from 'draft-js-plugins-editor';
import createLinkifyPlugin from 'draft-js-linkify-plugin';
import createEmojiPlugin from 'draft-js-emoji-plugin';
import createMentionPlugin from 'draft-js-mention-plugin'
import { EditorState, convertFromRaw, convertToRaw } from 'draft-js';
import { searchUser, getSearchQuery } from '../../actions/search'
import { fromJS } from 'immutable';
import { connect } from 'react-redux';
import MentionEntry from './MentionEntry';
import axios from 'axios';

import mentionsStyles from './MentionEntry/styles.css'
import editorStyles from './styles.scss'
import styles from '../../../assets/styles/styles.scss'
import './Emoji/styles.css';


const positionSuggestions = ({ state, props }) => {
  let transform;
  let transition;

  if (state.isActive && props.suggestions.size > 0) {
    transform = 'scaleY(1)';
    transition = 'all 0.25s cubic-bezier(.3,1.2,.2,1)';
  } else if (state.isActive) {
    transform = 'scaleY(0)';
    transition = 'all 0.25s cubic-bezier(.3,1,.2,1)';
  }

  return {
    transform,
    transition,
  };
};

const emojiPlugin = createEmojiPlugin();
const { EmojiSuggestions } = emojiPlugin;
const linkifyPlugin = createLinkifyPlugin({
    component: (props) => (
        <a {...props} onClick={() => console.log(props)} />
        ),
    target: '_blank'
});

const theme = {
    mention: 'mention',
    mentionSuggestions: 'mentionSuggestions',
    mentionSuggestionsEntryFocused: 'mentionSuggestionsEntryFocused',
    mentionSuggestionEntryText: 'mentionSuggestionEntryText',
    mentionSuggestionsEntryAvatar: 'mentionSuggestionEntryAvatar'
}

const mentionPlugin = createMentionPlugin({
    entityMutability: 'IMMUTABLE',
    theme: theme,
    positionSuggestions,
    mentionPrefix: '@',
})

const { MentionSuggestions } = mentionPlugin;

const plugins = [
  //linkifyPlugin,
  emojiPlugin,
  mentionPlugin
  ];

class TFNEditor extends React.Component {
    
    state = {
        editorState: EditorState.createEmpty(),
        suggestions: fromJS([])
    }

    onChange = (editorState) => {
        this.setState({
            editorState,
        });
        
       //console.log(convertToRaw(editorState.getCurrentContent()))
    };

    onSearchChange = ({value}) => {
        //console.log(this.props.token)
        if(value.length > 1)
        {
            var promise = searchUser(value, this.props.token);
            //console.log(promise);
                promise.then((result) => {
                    const data = result.data.map((user) => {
                        var a = {name: user.username, 
                            title: user.totalCredits, 
                            avatar: `https://pbs.twimg.com/profile_images/517863945/mattsailing_400x400.jpg`}
                        return a;
                    })
                    this.setState({
                        suggestions: fromJS(data)
                    });
                })
                .catch((error) => {
                    console.log(error)
                })  
        }
    }

    render() {
        return (
            <div className='editor' onClick={this.focus}>
                <Editor
                    editorState={this.state.editorState}
                    onChange={this.onChange}
                    plugins={plugins}
                    ref={(element) => { this.editor = element; }}
                />
                <MentionSuggestions
                    onSearchChange={this.onSearchChange}
                    suggestions={this.state.suggestions}
                    entryComponent={MentionEntry}
                />
                <EmojiSuggestions />
            </div>
        )
    }
}

function mapStateToProps(state) {
    const { token } = state.token;
    return {
        token,
    }
}

export default connect(mapStateToProps)(TFNEditor);