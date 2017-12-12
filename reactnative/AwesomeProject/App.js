import React from 'react';
import { StyleSheet, Alert, Text, View, Button } from 'react-native';

export default class App extends React.Component {
  render() {
    return (
      <View style={styles.container}>
        <Text style={styles.textStyle}>Hello React Native World! </Text>
        <View style={styles.buttonContainer}>
          <Button title = "Click me" color="#FFFFFF"
                  accessibilityLabel="Learn more about this purple button"
                  onPress={this.onPressLearnMore}/>
        </View>
      </View>
    );
  }

  onPressLearnMore(event) {
   Alert.alert("Hello");
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#5e5efe',
    alignItems: 'center',
    justifyContent: 'center',
  },
  textStyle: {
    color: 'white', 
    margin: 10, 
    fontSize: 20,
    fontWeight: 'bold'
  },
  buttonContainer: {
    backgroundColor: '#2E9218',
    borderRadius: 10,
    padding: 5,
    margin: 10,
    shadowColor: '#000000',
    shadowOffset: {
      width: 0,
      height: 3
    },
    shadowRadius: 10,
    shadowOpacity: 0.25
  }
});
